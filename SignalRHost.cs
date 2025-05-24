using Microsoft.Owin.Hosting;
using System;
using System.Windows.Forms;

namespace Collabry
{
    public class SignalRHost
    {
        private IDisposable _signalRServer;

        public void Start(string ip, int port, bool isVoice)
        {
            string url = $"http://{ip}:{port}/";

            if (isVoice)
                _signalRServer = WebApp.Start<VoiceSignalRStartup>(url);
            else
                _signalRServer = WebApp.Start<TextSignalRStartup>(url);

            MessageBox.Show("SignalR server started at " + url);
        }

        public void Stop()
        {
            _signalRServer?.Dispose();
        }
    }
}
