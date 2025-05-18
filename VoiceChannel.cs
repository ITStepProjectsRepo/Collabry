using Collabry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceChannel
    {
        public List<User_S> ConnectedUsers { get; set; } = new List<User_S>();
        public int MaxUsers { get; set; } = 25;

        private VoiceRelayServer relayServer;
        private readonly string relayIp;
        private readonly int relayPort;

        public VoiceChannel(string relayIp, int relayPort)
        {
            this.relayIp = relayIp;
            this.relayPort = relayPort;
        }

        public void Join(User_S user)
        {
            if (ConnectedUsers.Count >= MaxUsers) return;
            ConnectedUsers.Add(user);

            if (ConnectedUsers.Count == 1)
            {
                relayServer = new VoiceRelayServer(relayPort);
                relayServer.Start();
            }

            int receivePort = relayPort + 1;
            int sendPort = relayPort + 2;

            user.Receiver = new VoiceChatReceiver(receivePort);
            user.Receiver.Start();

            user.Sender = new VoiceChatSender(relayIp, relayPort, localPort: sendPort);
            if (!user.IsMuted && user.IsMicrophoneOn)
                user.Sender.Start();
        }

        public void Kick(User_S user)
        {
            ConnectedUsers.Remove(user);
            if (ConnectedUsers.Count == 0)
            {
                relayServer.Stop();
            }
            else
            {

            }
        }
    }
}
