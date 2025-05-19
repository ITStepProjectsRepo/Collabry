using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceChatSender
    {
        private WaveInEvent waveIn;
        private UdpClient udpClient;
        private string targetIp;
        private int targetPort;

        public VoiceChatSender(string targetIp, int targetPort, int localPort = 0)
        {
            this.targetIp = targetIp;
            this.targetPort = targetPort;

            if (localPort > 0)
                udpClient = new UdpClient(localPort);
            else
                udpClient = new UdpClient();

            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1) // 44.1kHz, mono
            };

            waveIn.DataAvailable += OnDataAvailable;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            // Console.WriteLine($"Captured {e.BytesRecorded} bytes");
            udpClient.Send(e.Buffer, e.BytesRecorded, targetIp, targetPort);
        }

        public void SendIntroPacket(User_S user)
        {
            var packet = new UserIntroPacket
            {
                Id = user.UserId,
                UserTag = user.User.UserTag,
                UserName = user.User.UserName,
                IsMuted = user.IsMuted,
                IsBanned = user.IsBanned,
                JoinedAt = user.JoinedAt,
                UserPictureData = user.User.UserPictureData
            };

            byte[] data = packet.ToBytes();
            udpClient.Send(data, data.Length, targetIp, targetPort);
        }

        public void Start() => waveIn.StartRecording();

        public void Stop()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            udpClient.Close();
        }
    }
}
