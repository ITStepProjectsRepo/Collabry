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
        private UdpClient udpServer;

        public VoiceRelayServer(int listenPort)
        {
            this.listenPort = listenPort;
        }

        public void Start()
        {
            udpServer = new UdpClient(listenPort);

            new System.Threading.Tasks.Task(() =>
            {
                while (true)
                {
                    var remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    var data = udpServer.Receive(ref remoteEP);

                    remoteEP = new IPEndPoint(remoteEP.Address, remoteEP.Port - 1);

                    if (!clients.Any(c => c.Address.Equals(remoteEP.Address) && c.Port == remoteEP.Port))
                    {
                        clients.Add(remoteEP);
                    }

                    foreach (var client in clients)
                    {
                        if (!client.Equals(remoteEP))
                        {
                            // Console.WriteLine($"Relay server received {data.Length} bytes from {remoteEP}. Forwarding to {client.Address}:{client.Port}");
                            udpServer.Send(data, data.Length, client);
                        }
                    }
                }
            }).Start();
        }

        public void Stop() => udpServer?.Close();
    }
}
