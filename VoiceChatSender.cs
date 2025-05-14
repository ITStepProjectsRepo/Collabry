using NAudio.Wave;
using System.Net.Sockets;

namespace Collabry
{
    public class VoiceChatSender
    {
        private WaveInEvent waveIn;
        private UdpClient udpClient;
        private string remoteIp;
        private int remotePort;

        public VoiceChatSender(string remoteIp, int remotePort)
        {
            this.remoteIp = remoteIp;
            this.remotePort = remotePort;
        }

        public void Start()
        {
            udpClient = new UdpClient();

            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(44100, 1) // 44.1kHz, mono
            };

            waveIn.DataAvailable += (s, a) =>
            {
                udpClient.Send(a.Buffer, a.BytesRecorded, remoteIp, remotePort);
            };

            waveIn.StartRecording();
        }

        public void Stop()
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            udpClient.Close();
        }
    }
}
