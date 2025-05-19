using Collabry;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Collabry
{
    public class VoiceChannelClient
    {
        private VoiceRelayServer relayServer;

        public VoiceChannel CurrentVoiceChannel { get; set; }
            
        public User_S CurrentUser { get; set; }

        public VoiceChatSender Sender { get; set; }

        public VoiceChatReceiver Receiver { get; set; }

        public bool IsMicrophoneOn { get; private set; } = true;
        public bool IsServer { get; set; }

        public void TurnMicOff()
        {
            IsMicrophoneOn = false;
            Sender?.Stop();
        }

        public void TurnMicOn()
        {
            if (!CurrentUser.IsMuted)
            {
                IsMicrophoneOn = true;
                Sender?.Start();
            }
        }

        public List<UserIntroPacket> ConnectedUsers { get; set; } = new List<UserIntroPacket>();

        private HubConnection hubConnection;

        public VoiceChannelClient(VoiceChannel channel, User_S currentUser, int relayPort = 5000)
        {
            CurrentVoiceChannel = channel;
            CurrentUser = currentUser;
            IsServer = false;

            if (string.IsNullOrEmpty(CurrentVoiceChannel.RelayIp) || CurrentVoiceChannel.RelayPort == 0)
            {
                _ = BecomeServerAsync(relayPort);
            }
        }

        public void Connect()
        {
            int receivePort = CurrentVoiceChannel.RelayPort + 1;
            int sendPort = CurrentVoiceChannel.RelayPort + 2;

            Receiver = new VoiceChatReceiver(receivePort);
            Receiver.Start();

            Sender = new VoiceChatSender(CurrentVoiceChannel.RelayIp, CurrentVoiceChannel.RelayPort, localPort: sendPort);
            if (!CurrentUser.IsMuted && IsMicrophoneOn)
                Sender.Start();
        }

        private async System.Threading.Tasks.Task BecomeServerAsync(int relayPort)
        {
            CurrentVoiceChannel.RelayPort = relayPort;

            relayServer = new VoiceRelayServer(CurrentVoiceChannel.RelayPort);
            relayServer.Start();

            IsServer = true;

            using (var socket = new UdpClient())
            {
                socket.Connect("8.8.8.8", 65530);
                var endPoint = socket.Client.LocalEndPoint as IPEndPoint;
                CurrentVoiceChannel.RelayIp = endPoint.Address.ToString();
            }

            VoiceChannelService.UpdateRelaySettings(CurrentVoiceChannel.Id, CurrentVoiceChannel.RelayIp, CurrentVoiceChannel.RelayPort);

            // Запускаем обновление списка пользователей
            _ = System.Threading.Tasks.Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (hubConnection == null || hubConnection.State == HubConnectionState.Disconnected)
                        {
                            await InitializeHubAsync();
                        }

                        var list = relayServer.UserMap.Values.ToList();

                        await hubConnection.SendAsync("UpdateUserList", list);
                        await System.Threading.Tasks.Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in user update loop: {ex.Message}");
                    }
                }
            });
        }

        private bool ShouldBecomeServer()
        {
            if (ConnectedUsers == null || ConnectedUsers.Count == 0)
                return true;

            var minId = ConnectedUsers.Min(u => u.Id);
            return CurrentUser.Id <= minId;
        }

        public async System.Threading.Tasks.Task InitializeHubAsync()
        {
            await InitializeHubConnectionAsync();
        }

        private async System.Threading.Tasks.Task InitializeHubConnectionAsync()
        {
            string url = $"http://{CurrentVoiceChannel.RelayIp}:{CurrentVoiceChannel.RelayPort}/voicehub";

            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<List<UserIntroPacket>>("UserListUpdated", users => ConnectedUsers = users);

            hubConnection.Closed += OnHubConnectionClosed;

            await hubConnection.StartAsync();
        }

        private async System.Threading.Tasks.Task OnHubConnectionClosed(Exception error)
        {
            await System.Threading.Tasks.Task.Delay(2000); // время на auto reconnect

            if (ShouldBecomeServer())
            {
                await BecomeServerAsync(CurrentVoiceChannel.RelayPort);
            }
            else
            {
                var settings = VoiceChannelService.GetRelaySettings(CurrentVoiceChannel.Id);
                if (settings.HasValue)
                {
                    string relayIp = settings.Value.relayIp;
                    int relayPort = settings.Value.relayPort;

                    if (!string.IsNullOrEmpty(relayIp) && relayPort > 0)
                    {
                        CurrentVoiceChannel.RelayIp = relayIp;
                        CurrentVoiceChannel.RelayPort = relayPort;

                        try
                        {
                            await InitializeHubConnectionAsync(); // повторное подключение
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error reconnecting to hub: {ex.Message}");
                        }
                    }
                }
            }
        }


        public void Mute()
        {
            CurrentUser.IsMuted = true;
            Sender?.Stop();
        }

        public void Unmute()
        {
            CurrentUser.IsMuted = false;
            if (IsMicrophoneOn)
                Sender?.Start();
        }
    }
}
