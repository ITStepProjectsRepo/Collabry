using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceRelayServer
    {
        private readonly int listenPort;

        public readonly Dictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)> userMap =
            new Dictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)>();
        public IReadOnlyDictionary<IPEndPoint, (UserIntroPacket Packet, DateTime LastSeen)> UserMap => userMap;

        private UdpClient udpServer;

        public event Action<IPEndPoint, UserIntroPacket> ClientConnected;
        public event Action<IPEndPoint, UserIntroPacket> ClientDisconnected;

        public VoiceRelayServer(int listenPort)
        {
            this.listenPort = listenPort;
        }

        public void Start()
        {
            udpServer = new UdpClient(listenPort);

            System.Threading.Tasks.Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var remoteEP = new IPEndPoint(IPAddress.Any, 0);
                        var data = udpServer.Receive(ref remoteEP);

                        remoteEP = new IPEndPoint(remoteEP.Address, remoteEP.Port - 1);

                        if (!userMap.ContainsKey(remoteEP))
                        {
                            try
                            {
                                var intro = UserIntroPacket.FromBytes(data);
                                userMap[remoteEP] = (intro, DateTime.UtcNow);
                                ClientConnected?.Invoke(remoteEP, intro);
                                continue;
                            }
                            catch {   }
                        }
                        else
                        {
                            var existing = userMap[remoteEP];
                            userMap[remoteEP] = (existing.Packet, DateTime.UtcNow);
                        }

                        foreach (var client in userMap.Keys)
                        {
                            if (!client.Equals(remoteEP))
                            {
                                udpServer.Send(data, data.Length, client);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[VoiceRelayServer]: {ex.Message}");
                    }
                }
            });

            // Очистка "отсохших"
            System.Threading.Tasks.Task.Run(async () =>
            {
                while (true)
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

                    await System.Threading.Tasks.Task.Delay(5000);
                }
            });
        }

        public void Stop()
        {
            udpServer?.Close();
            udpServer = null;
            userMap.Clear();
        }
    }
}
