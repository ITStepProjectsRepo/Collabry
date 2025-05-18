using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            // Console.WriteLine($"Receiver listening on port {listenPort}");

            waveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
            waveOut = new WaveOutEvent();
            waveOut.Init(waveProvider);
            waveOut.Play();

            isRunning = true;

            Thread receiveThread = new Thread(() =>
            {
                while (isRunning)
                {
                    // Console.WriteLine($"Waiting for data...");
                    IPEndPoint remoteEP = null;
                    byte[] receivedData = udpClient.Receive(ref remoteEP);
                    // Console.WriteLine($"Received {receivedData.Length} bytes from {remoteEP}");
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
