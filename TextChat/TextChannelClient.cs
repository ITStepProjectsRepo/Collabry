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
    public class TextChannelClient
    {
        public TextChannel CurrentTextChannel { get; set; }
        public User_S CurrentUser { get; set; }

        public BindingList<UserIntroPacket> ConnectedUsers { get; set; } = new BindingList<UserIntroPacket>();
        private Form _uiForm;

        private HubConnection hubConnection;
        private IHubProxy hubProxy;

        private SignalRHost signalRHost;

        private bool IsServer;
        private bool isClosing = false;

        public event Action<Message_S> OnMessageReceived;

        public TextChannelClient(User_S currentUser, Form uiForm)
        {
            CurrentUser = currentUser;
            _uiForm = uiForm;
        }

        public async Task Connect(int channelId)
        {
            CurrentTextChannel = TextChannelService.GetTextChannelById(channelId);

            await InitializeHubAsync();
        }

        public void Disconnect()
        {
            try
            {
                isClosing = true;

                if (hubConnection != null)
                {
                    if (hubConnection.State == ConnectionState.Connected || hubConnection.State == ConnectionState.Connecting)
                        hubConnection.Stop();

                    hubConnection.Dispose();
                    hubConnection = null;
                    hubProxy = null;
                }

                if (IsServer)
                {
                    signalRHost?.Stop();
                    signalRHost = null;
                    IsServer = false;

                    TextChannelService.UpdateRelaySettings(CurrentTextChannel.Id, null, 0);
                }
            }
            catch { }
        }

        private async Task InitializeHubAsync(int relayPort = 6000)
        {
            if (string.IsNullOrEmpty(CurrentTextChannel.RelayIp) || CurrentTextChannel.RelayPort == 0)
            {
                await BecomeServerAsync(relayPort);
            }

            await InitializeHubConnectionAsync();
        }

        private async Task InitializeHubConnectionAsync()
        {
            hubConnection = new HubConnection($"http://{CurrentTextChannel.RelayIp}:{CurrentTextChannel.RelayPort}/texthub");
            hubProxy = hubConnection.CreateHubProxy("TextHub");

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

            hubProxy.On<Message_S>("ReceiveMessage", message =>
            {
                OnMessageReceived?.Invoke(message);
            });

            hubConnection.Closed += async () => await OnHubConnectionClosed();

            await hubConnection.Start();

            await hubProxy.Invoke("Register", new UserIntroPacket
            {
                Id = CurrentUser.User.Id,
                UserName = CurrentUser.User.UserName,
                UserTag = CurrentUser.User.UserTag,
                IsMuted = CurrentUser.IsMuted,
                IsBanned = CurrentUser.IsBanned,
                JoinedAt = CurrentUser.JoinedAt,
                UserPictureData = CurrentUser.User.UserPictureData
            });
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
                await BecomeServerAsync(CurrentTextChannel.RelayPort);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var settings = TextChannelService.GetRelaySettings(CurrentTextChannel.Id);
                    if (settings.HasValue && !string.IsNullOrEmpty(settings.Value.relayIp) && settings.Value.relayPort != 0)
                    {
                        CurrentTextChannel.RelayIp = settings.Value.relayIp;
                        CurrentTextChannel.RelayPort = settings.Value.relayPort;
                        await InitializeHubConnectionAsync();
                        return;
                    }

                    await Task.Delay(1000);
                }
            }
        }

        public async Task SendMessageAsync(string content)
        {
            var message = new Message_S
            {
                Sender = CurrentUser.User.UserName,
                Text = content,
                TextChannelId = CurrentTextChannel.Id
            };

            await hubProxy.Invoke("SendMessage", message);
        }

        public void LoadHistory()
        {
            var history = TextChannelService.GetMessagesByChannelId(CurrentTextChannel.Id);
            foreach (var msg in history)
            {
                OnMessageReceived?.Invoke(msg);
            }
        }

        private async Task BecomeServerAsync(int relayPort)
        {
            CurrentTextChannel.RelayPort = relayPort;
            IsServer = true;

            using (var socket = new UdpClient())
            {
                socket.Connect("8.8.8.8", 65530);
                var endPoint = socket.Client.LocalEndPoint as IPEndPoint;
                CurrentTextChannel.RelayIp = endPoint?.Address.ToString();
            }

            signalRHost = new SignalRHost();
            signalRHost.Start(CurrentTextChannel.RelayIp, CurrentTextChannel.RelayPort, false);

            TextChannelService.UpdateRelaySettings(CurrentTextChannel.Id, CurrentTextChannel.RelayIp, CurrentTextChannel.RelayPort);

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (hubConnection == null || hubConnection.State == ConnectionState.Disconnected)
                            await InitializeHubAsync();

                        if (hubConnection.State == ConnectionState.Connected)
                        {
                            var list = ConnectedUsers.ToList();
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

            var others = ConnectedUsers.Where(u => u.Id != CurrentUser.Id).ToList();
            if (others.Count == 0)
                return true;

            return CurrentUser.Id <= others.Min(u => u.Id);
        }
    }
}
