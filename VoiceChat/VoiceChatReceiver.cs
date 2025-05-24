using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Collabry
{
    public class VoiceChatReceiver
    {
        private UdpClient udpClient;
        private BufferedWaveProvider waveProvider;
        private WaveOutEvent waveOut;
        private int listenPort;
        private volatile bool isRunning;

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
                    try
                    {
                        // Console.WriteLine($"Waiting for data...");
                        IPEndPoint remoteEP = null;
                        byte[] receivedData = udpClient.Receive(ref remoteEP);
                        // Console.WriteLine($"Received {receivedData.Length} bytes from {remoteEP}");
                        waveProvider.AddSamples(receivedData, 0, receivedData.Length);
                    }
                    catch (SocketException) { break; }
                    catch (ObjectDisposedException) { break; }
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
