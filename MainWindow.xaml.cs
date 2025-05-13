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
            
            //u.Connections.Add(localServer, _);
            //u.UserDMSetting.Add(localServer, false);
            Debug.WriteLine(u.ToString());
            u.NewForm();
            //string v = u.MakeInvitation();
            //u.AddDM(v);

            //using (var db = new AppDbContext())
            //{
            //    TextChannel channel = new TextChannel { Name = "General" };
            //    db.TextChannels.Add(channel);
            //    db.SaveChanges();

            //    var msg = new Message
            //    {
            //        Text = "First message!",
            //        SendTime = DateTime.Now,
            //        Sender = "Vasya",
            //        TextChannelId = channel.Id
            //    };

            //    db.Messages.Add(msg);
            //    db.SaveChanges();
            //}
        }
    }
}
