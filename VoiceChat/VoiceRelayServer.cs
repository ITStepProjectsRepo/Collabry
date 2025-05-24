using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceRelayServer
    {
        private readonly int listenPort;
        private UdpClient udpServer;
        private volatile bool isRunning;

        public readonly Dictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)> userMap =
            new Dictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)>();

        public IReadOnlyDictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)> UserMap => userMap;

        public event Action<IPEndPoint, UserIntroPacket> ClientConnected;
        public event Action<IPEndPoint, UserIntroPacket> ClientDisconnected;

        public VoiceRelayServer(int listenPort)
        {
            this.listenPort = listenPort;
        }

        public void Start()
        {
            udpServer = new UdpClient(listenPort);
            isRunning = true;

            Task.Run(ReceiveLoop);
            Task.Run(RemoveInactiveClientsLoop);
        }

        private void ReceiveLoop()
        {
            while (isRunning)
            {
                try
                {
                    var remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    var data = udpServer.Receive(ref remoteEP);
                    if (data.Length == 0) continue;

                    remoteEP = new IPEndPoint(remoteEP.Address, remoteEP.Port - 1);

                    byte packetType = data[0];
                    byte[] payload = data.Skip(1).ToArray();

                    switch (packetType)
                    {
                        case 1: // Intro packet
                            HandleIntroPacket(remoteEP, payload);
                            break;

                        case 2: // Audio packet
                            HandleAudioPacket(remoteEP, payload);
                            break;

                        default:
                            Console.WriteLine($"[VoiceRelayServer] Unknown packet type: {packetType}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[VoiceRelayServer]: {ex.Message}");
                }
            }
        }

        private void HandleIntroPacket(IPEndPoint remoteEP, byte[] payload)
        {
            UserIntroPacket intro;
            try
            {
                intro = UserIntroPacket.FromBytes(payload);
            }
            catch
            {
                Console.WriteLine("[VoiceRelayServer] Failed to parse intro packet.");
                return;
            }

            bool isNewClient = !userMap.ContainsKey(remoteEP);
            userMap[remoteEP] = (intro, DateTime.UtcNow);

            if (isNewClient)
            {
                ClientConnected?.Invoke(remoteEP, intro);
            }
        }

        private void HandleAudioPacket(IPEndPoint sender, byte[] payload)
        {
            if (userMap.ContainsKey(sender))
            {
                // update last seen
                var oldData = userMap[sender];
                userMap[sender] = (oldData.Packet, DateTime.UtcNow);

                foreach (var client in userMap.Keys)
                {
                    if (!client.Equals(sender))
                    {
                        try
                        {
                            udpServer.Send(payload, payload.Length, client);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[VoiceRelayServer] Failed to relay audio: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                // ignore unknown sender
                Console.WriteLine($"[VoiceRelayServer] Audio packet from unknown sender: {sender}");
            }
        }

        private async Task RemoveInactiveClientsLoop()
        {
            while (isRunning)
            {
                var now = DateTime.UtcNow;
                var deadClients = userMap
                    .Where(pair => (now - pair.Value.LastSeen).TotalSeconds > 10)
                    .Select(pair => pair.Key)
                    .ToList();

                foreach (var client in deadClients)
                {
                    if (userMap.TryGetValue(client, out var data))
                    {
                        userMap.Remove(client);
                        ClientDisconnected?.Invoke(client, data.Packet);
                    }
                }

                await Task.Delay(5000);
            }
        }

        public void Stop()
        {
            isRunning = false;
            udpServer?.Close();
            udpServer = null;
            userMap.Clear();
        }
    }
}
