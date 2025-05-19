using System;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Collabry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            User u = new User() {
                UserTag = "qwerty123",
                UserName = "qwerty123",
                Email = "admin",
                Password = "admin",
                UserPicture = new System.Drawing.Bitmap(1, 1),
                UserInfo = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer at tortor ut orci feugiat commodo non ornare leo.",
            };

            MeetingWindow meetingWindow = new MeetingWindow(); meetingWindow.Show();

            //u.Connections.Add(localServer, _);
            //u.UserDMSetting.Add(localServer, false);
            //Debug.WriteLine(u.ToString());
            //u.NewForm();
            //string v = u.MakeInvitation();
            //u.AddDM(v);

            // Voice test (VoiceChatSender + VoiceChatReceiver)
            /*
            // Sender
            var sender = new VoiceChatSender("127.0.0.1", 5000);
            sender.Start();

            // Receiver
            var receiver = new VoiceChatReceiver(5000);
            receiver.Start();
            */

            // Server roles, users and others test (DB is now broken)
            /*
            var user = UserService.AddUser("#0001", "Name", "Email", "12345");
            var server = ServerService.CreateServer("My Server");
            User_SService.AddUserToServer(user.Id, server.Id);

            var mod = ServerRoleService.AddRole("Модератор", "Следит за порядком", canKickUsers: true, canBanUsers: true);
            ServerRoleService.AssignRoleToUser(userId: user.Id, roleId: mod.Id);

            var textChannel = ServerTextChannelService.CreateTextChannel(server.Id, "Chat");

            var messageService = new Message_SService();
            messageService.AddMessage(new Message_S { Text = "Hello world!", Sender = user.UserName, TextChannelId = textChannel.Id });
            */

            // Voice test 2 (VoiceChannel RelayServer + Client)
            /*
            string relayIp = "192.168.0.111";
            int relayPort = 5000;

            var channel = new VoiceChannel(relayIp, relayPort);

            var user = new User_S { Id = 1 };

            channel.Join(user);

            Console.WriteLine("Voice channel joined. Press Enter to leave...");
            Console.ReadLine();

            channel.Kick(user);
            */

            // Voice test 2 (VoiceChannel Client)
            /*
            string relayIp = "192.168.0.111";
            int relayPort = 5000;

            var user = new User_S { Id = 2 };

            int receivePort = relayPort + 1;
            int sendPort = relayPort + 2;

            user.Receiver = new VoiceChatReceiver(receivePort);
            user.Receiver.Start();

            user.Sender = new VoiceChatSender(relayIp, relayPort, localPort: sendPort);
            user.Sender.Start();

            Console.WriteLine("Client started. Press Enter to exit...");
            Console.ReadLine();

            user.Sender.Stop();
            user.Receiver.Stop();
            */
        }
    }
}
