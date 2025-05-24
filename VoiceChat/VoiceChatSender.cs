using NAudio.Wave;
using System;
using System.Net.Sockets;

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
            byte[] audioPayload = new byte[e.BytesRecorded + 1];
            audioPayload[0] = 2; // Type 2 = audio packet
            Buffer.BlockCopy(e.Buffer, 0, audioPayload, 1, e.BytesRecorded);

            udpClient.Send(audioPayload, audioPayload.Length, targetIp, targetPort);
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

            byte[] introData = packet.ToBytes();
            byte[] fullPacket = new byte[introData.Length + 1];
            fullPacket[0] = 1; // Type 1 = intro packet
            Buffer.BlockCopy(introData, 0, fullPacket, 1, introData.Length);

            udpClient.Send(fullPacket, fullPacket.Length, targetIp, targetPort);
        }
        public void Start() => waveIn.StartRecording();

        public void StopRecording()
        {
            waveIn.StopRecording();
        }

        public void Stop()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            udpClient.Close();
        }
    }
}
