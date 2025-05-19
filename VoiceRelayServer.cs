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
        private readonly List<IPEndPoint> clients = new List<IPEndPoint>();
        public readonly Dictionary<IPEndPoint, UserIntroPacket> userMap = new Dictionary<IPEndPoint, UserIntroPacket>();
        public IReadOnlyDictionary<IPEndPoint, UserIntroPacket> UserMap => userMap;
        private UdpClient udpServer;

        public event Action<IPEndPoint, UserIntroPacket> ClientConnected;

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
                                userMap[remoteEP] = intro;
                                clients.Add(remoteEP);

                                ClientConnected?.Invoke(remoteEP, intro);

                                continue;
                            }
                            catch
                            {
                                // Not a valid intro packet, ignore or handle differently
                            }
                        }

                        foreach (var client in clients)
                        {
                            if (!client.Equals(remoteEP))
                            {
                                udpServer.Send(data, data.Length, client);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[VoiceRelayServer] Error: {ex.Message}");
                    }
                }
            });
        }

        public void Stop()
        {
            udpServer?.Close();
            udpServer = null;
            clients.Clear();
            userMap.Clear();
        }
    }
}
