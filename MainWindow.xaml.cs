using System;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace Collabry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User Device {  get; set; }
        public static User Device_S {  get; set; }

        private VoiceChannelClient voiceChannelClient;
        private TextChannelClient textChannelClient;
        private BindingList<Message_S> messages = new BindingList<Message_S>();
        public MainWindow()
        {
            this.Hide();
            Login log = new Login();
            var res = log.ShowDialog();
            InitializeComponent();
            if (res == System.Windows.Forms.DialogResult.Yes)
            {
                Device = Device_S;
                Device.SaveUserData();
                Debug.WriteLine(Device.ToString());
                //Device.NewForm();
                this.ShowDialog();
            }
            else if (res == System.Windows.Forms.DialogResult.OK)
            {
                Device = Device_S;
                Debug.WriteLine(Device.ToString());
                //Device.NewForm();
                this.ShowDialog();
            }

            //MeetingWindow meetingWindow = new MeetingWindow(); meetingWindow.Show();


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

            // Voice chat example (Windows Forms)
            /*
            var user = new User("#0002", "User1", "Email", "12345", null, null);
            UserService.AddUser(user);

            var user_s = ServerService.AddUserToServer(69, user.Id);

            voiceClient = new VoiceChannelClient(user_s, this);
            await voiceClient.Connect(69);

            labelStatus.Text = "Connected";

            listBoxConnectedUsers.DataSource = voiceClient.ConnectedUsers;
            listBoxConnectedUsers.DisplayMember = "UserName";
            */
        }
    }
}
