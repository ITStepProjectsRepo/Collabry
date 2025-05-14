using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Collabry
{
    

    public class Meeting
    {
        public int Id { get; set; }
        public string MeetingName { get; set; }
        public DateTime MeetingTime { get; set; }
        public TimeSpan MeetingDuration { get; set; }
        public List<string> InvitedTags { get; set; }
        public string JoinUri => $"{MakeInvitation()}";

        public bool OnMic { get; set; }
        public bool OnCam { get; set; }
        public bool OnScreenShare { get; set; }

        private TcpListener listener;
        private int listeningPort;
        private string localIP;

        private List<TcpClient> clients = new List<TcpClient>();
        private List<NetworkStream> streams = new List<NetworkStream>();
        private List<CancellationTokenSource> tokenSources = new List<CancellationTokenSource>();

        private string JoinLink => $"{localIP}:{listeningPort}";

        public Meeting() { }

        public Meeting(int id, string meetingName, DateTime meetingTime, TimeSpan meetingDuration, List<string> invitedTags)
        {
            Id = id;
            MeetingName = meetingName;
            MeetingTime = meetingTime;
            MeetingDuration = meetingDuration;
            InvitedTags = invitedTags;
        }

        public string MakeInvitation()
        {
            string cipherKey = ClientHandler.GenerateRandomString(8);
            string header = string.Join("//", new[] { "https:", "clbry.meet" });
            string invite = string.Join("||", new[] { $"{header}", $"{ClientHandler.VigenereEncrypt($"{localIP}:4890", cipherKey)}" });
            invite = string.Join("#", new[] { $"{invite}", $"{cipherKey}" });
            return invite;
        }

        public void Start(int port)
        {
            listeningPort = port;
            localIP = GetLocalIPAddress();
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Listening on {localIP}:{listeningPort}");
            AcceptClientsLoop();
        }

        public void ConnectToPeer(string ip, int port)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(ip, port);
                NetworkStream stream = client.GetStream();

                clients.Add(client);
                streams.Add(stream);
                var tokenSource = new CancellationTokenSource();
                tokenSources.Add(tokenSource);

                Console.WriteLine($"Connected to peer {ip}:{port}");
                StartMediaLoops(stream, tokenSource.Token);
                StartReceiveLoop(stream, tokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConnectToPeer error: {ex.Message}");
            }
        }

        private async void AcceptClientsLoop()
        {
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();

                clients.Add(client);
                streams.Add(stream);
                var tokenSource = new CancellationTokenSource();
                tokenSources.Add(tokenSource);

                Console.WriteLine($"Peer connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

                await System.Threading.Tasks.Task.Run(() => StartMediaLoops(stream, tokenSource.Token));
                await System.Threading.Tasks.Task.Run(() => StartReceiveLoop(stream, tokenSource.Token));
            }
        }

        private void StartMediaLoops(NetworkStream stream, CancellationToken token)
        {
            if (OnMic)
                _ = System.Threading.Tasks.Task.Run(() => SendMicLoop(stream, token));

            if (OnCam)
                _ = System.Threading.Tasks.Task.Run(() => SendCamLoop(stream, token));

            if (OnScreenShare)
                _ = System.Threading.Tasks.Task.Run(() => SendScreenLoop(stream, token));
        }

        private async void SendMicLoop(NetworkStream stream, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                byte[] audio = GetDummyAudioData();
                await System.Threading.Tasks.Task.Run(() => SendPacketAsync("MIC", audio, stream));
                await System.Threading.Tasks.Task.Delay(50);
            }
        }

        private async void SendCamLoop(NetworkStream stream, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                byte[] video = GetDummyCameraData();
                await System.Threading.Tasks.Task.Run(() => SendPacketAsync("CAM", video, stream));
                await System.Threading.Tasks.Task.Delay(100);
            }
        }

        private async void SendScreenLoop(NetworkStream stream, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                byte[] screen = GetDummyScreenData();
                await System.Threading.Tasks.Task.Run(() => SendPacketAsync("SCR", screen, stream));
                await System.Threading.Tasks.Task.Delay(200);
            }
        }

        private void StartReceiveLoop(NetworkStream stream, CancellationToken token)
        {
            var reader = new BinaryReader(stream);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    string type = new string(reader.ReadChars(3));
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);

                    HandleIncomingData(type, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Receive error: {ex.Message}");
                    break;
                }
            }
        }

        private void HandleIncomingData(string type, byte[] data)
        {
            switch (type)
            {
                case "MIC":
                    Console.WriteLine("[MIC] Received mic data");
                    break;
                case "CAM":
                    Console.WriteLine("[CAM] Received camera frame");
                    break;
                case "SCR":
                    Console.WriteLine("[SCR] Received screen share");
                    break;
                default:
                    Console.WriteLine($"[??] Unknown packet type: {type}");
                    break;
            }
        }

        private async void SendPacketAsync(string type, byte[] data, NetworkStream stream)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write(type.ToCharArray(), 0, 3);
                    writer.Write(data.Length);
                    writer.Write(data);
                    writer.Flush();

                    byte[] packet = ms.ToArray();
                    await stream.WriteAsync(packet, 0, packet.Length);
                    await stream.FlushAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send error: {ex.Message}");
            }
        }

        // Dummy data generation methods for simulation
        private byte[] GetDummyAudioData() => new byte[128];
        private byte[] GetDummyCameraData() => new byte[512];
        private byte[] GetDummyScreenData() => new byte[1024];

        private string GetLocalIPAddress()
        {
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();

            throw new Exception("Local IP Address Not Found!");
        }
    }
}