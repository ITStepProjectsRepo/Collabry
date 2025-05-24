using Collabry;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collabry
{
    public class VoiceChannelClient
    {
        public VoiceChannel CurrentVoiceChannel { get; set; }
        public User_S CurrentUser { get; set; }
        public bool IsMicrophoneOn { get; private set; } = true;

        public BindingList<UserIntroPacket> ConnectedUsers { get; set; } = new BindingList<UserIntroPacket>();
        private Form _uiForm;

        private VoiceRelayServer relayServer;
        private SignalRHost signalRHost;
        private VoiceChatSender Sender;
        private VoiceChatReceiver Receiver;
        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        private bool IsServer;
        private bool isClosing = false;

        private System.Timers.Timer introTimer;

        public VoiceChannelClient(User_S currentUser, Form uiForm)
        {
            CurrentUser = currentUser;
            _uiForm = uiForm;
        }

        public async Task Connect(int channelId)
        {
            CurrentVoiceChannel = VoiceChannelService.GetVoiceChannelById(channelId);

            await InitializeHubAsync();

            StartVoiceNetworking();
        }

        private void StartVoiceNetworking()
        {
            int receivePort = CurrentVoiceChannel.RelayPort + 1;
            int sendPort = CurrentVoiceChannel.RelayPort + 2;

            Receiver = new VoiceChatReceiver(receivePort);
            Receiver.Start();

            Sender = new VoiceChatSender(CurrentVoiceChannel.RelayIp, CurrentVoiceChannel.RelayPort, sendPort);
            Sender.SendIntroPacket(CurrentUser);

            if (!CurrentUser.IsMuted && IsMicrophoneOn)
            {
                Sender.Start();
            }
            else if (CurrentUser.IsMuted)
            {
                StartIntroTimer();
            }
        }

        public void Disconnect()
        {
            try
            {
                isClosing = true;

                StopIntroTimer();

                Sender?.Stop();
                Sender = null;

                Receiver?.Stop();
                Receiver = null;

                if (hubConnection != null)
                {
                    if (hubConnection.State == ConnectionState.Connected ||
                        hubConnection.State == ConnectionState.Connecting)
                    {
                        hubConnection.Stop();
                    }

                    hubConnection.Dispose();
                    hubConnection = null;
                    hubProxy = null;
                }

                if (IsServer)
                {
                    signalRHost?.Stop();
                    relayServer?.Stop();
                    signalRHost = null;
                    relayServer = null;

                    IsServer = false;

                    VoiceChannelService.UpdateRelaySettings(CurrentVoiceChannel.Id, null, 0);
                }
            }
            catch
            {

            }
        }

        public async Task InitializeHubAsync(int relayPort = 5000)
        {
            if (string.IsNullOrEmpty(CurrentVoiceChannel.RelayIp) || CurrentVoiceChannel.RelayPort == 0)
            {
                await BecomeServerAsync(relayPort);
            }

            await InitializeHubConnectionAsync();
        }

        private async Task InitializeHubConnectionAsync()
        {
            hubConnection = new HubConnection($"http://{CurrentVoiceChannel.RelayIp}:{CurrentVoiceChannel.RelayPort + 100}/voicehub");
            hubProxy = hubConnection.CreateHubProxy("VoiceHub");

            hubProxy.On<List<UserIntroPacket>>("UserListUpdated", users =>
            {
                if (_uiForm.InvokeRequired)
                {
                    _uiForm.Invoke(new Action(() =>
                    {
                        ConnectedUsers.Clear();
                        foreach (var user in users)
                            ConnectedUsers.Add(user);
                    }));
                }
                else
                {
                    ConnectedUsers.Clear();
                    foreach (var user in users)
                        ConnectedUsers.Add(user);
                }
            });

            hubConnection.Closed += async () => await OnHubConnectionClosed();
            try
            {
                await hubConnection.Start();
            }
            catch
            {

            }
        }

        private async Task OnHubConnectionClosed()
        {
            if (isClosing)
            {
                return;
            }

            await Task.Delay(2000);

            Disconnect();

            if (ShouldBecomeServer())
            {
                await BecomeServerAsync(CurrentVoiceChannel.RelayPort);
                StartVoiceNetworking();
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var settings = VoiceChannelService.GetRelaySettings(CurrentVoiceChannel.Id);
                    if (settings.HasValue &&
                        !string.IsNullOrEmpty(settings.Value.relayIp) &&
                        settings.Value.relayPort != 0)
                    {
                        CurrentVoiceChannel.RelayIp = settings.Value.relayIp;
                        CurrentVoiceChannel.RelayPort = settings.Value.relayPort;
                        await InitializeHubConnectionAsync();
                        return;
                    }

                    await Task.Delay(1000);
                }
            }
        }

        private async Task BecomeServerAsync(int relayPort)
        {
            CurrentVoiceChannel.RelayPort = relayPort;
            relayServer = new VoiceRelayServer(relayPort);
            relayServer.Start();
            IsServer = true;

            using (var socket = new UdpClient())
            {
                socket.Connect("8.8.8.8", 65530);
                var endPoint = socket.Client.LocalEndPoint as IPEndPoint;
                CurrentVoiceChannel.RelayIp = endPoint?.Address.ToString();
            }

            signalRHost = new SignalRHost();
            signalRHost.Start(CurrentVoiceChannel.RelayIp, CurrentVoiceChannel.RelayPort + 100, true);

            VoiceChannelService.UpdateRelaySettings(CurrentVoiceChannel.Id, CurrentVoiceChannel.RelayIp, CurrentVoiceChannel.RelayPort);

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (hubConnection == null || hubConnection.State == ConnectionState.Disconnected)
                        {
                            await InitializeHubAsync();
                        }

                        if (hubConnection.State == ConnectionState.Connected)
                        {
                            var list = relayServer.UserMap.Values.Select(v => v.Packet).ToList();
                            await hubProxy.Invoke("UpdateUserList", list);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"User update error: {ex.Message}");
                    }

                    await Task.Delay(1000);
                }
            });
        }

        private bool ShouldBecomeServer()
        {
            if (ConnectedUsers == null || ConnectedUsers.Count == 0)
                return true;

            var otherUsers = ConnectedUsers
                .Skip(1)
                .Where(u => u.Id != CurrentUser.Id)
                .ToList();

            if (otherUsers.Count == 0)
                return true;

            return CurrentUser.Id <= otherUsers.Min(u => u.Id);
        }

        private void StartIntroTimer()
        {
            if (introTimer != null || Sender == null)
                return;

            introTimer = new System.Timers.Timer(1000);
            introTimer.Elapsed += (s, e) =>
            {
                if (CurrentUser.IsMuted)
                    Sender.SendIntroPacket(CurrentUser);
            };
            introTimer.AutoReset = true;
            introTimer.Start();
        }

        private void StopIntroTimer()
        {
            introTimer?.Stop();
            introTimer?.Dispose();
            introTimer = null;
        }


        public void Mute()
        {
            CurrentUser.IsMuted = true;
            Sender?.StopRecording();
            StartIntroTimer();
        }

        public void Unmute()
        {
            CurrentUser.IsMuted = false;
            if (IsMicrophoneOn)
                Sender?.Start();
            StopIntroTimer();
            Sender.SendIntroPacket(CurrentUser);
        }
    }
}
