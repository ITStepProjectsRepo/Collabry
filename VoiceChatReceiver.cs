using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Collabry
{
    public class VoiceChatReceiver
    {
        private UdpClient udpClient;
        private BufferedWaveProvider waveProvider;
        private WaveOutEvent waveOut;
        private int listenPort;
        private bool isRunning;

        public VoiceChatReceiver(int listenPort)
        {
            this.listenPort = listenPort;
        }

        public void Start()
        {
            udpClient = new UdpClient(listenPort);
            waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
            waveOut = new WaveOutEvent();
            waveOut.Init(waveProvider);
            waveOut.Play();

            isRunning = true;

            Thread receiveThread = new Thread(() =>
            {
                while (isRunning)
                {
                    IPEndPoint remoteEP = null;
                    byte[] receivedData = udpClient.Receive(ref remoteEP);
                    waveProvider.AddSamples(receivedData, 0, receivedData.Length);
                }
            });

            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void Stop()
        {
            isRunning = false;
            udpClient.Close();
            waveOut.Stop();
            waveOut.Dispose();
        }
    }
}
