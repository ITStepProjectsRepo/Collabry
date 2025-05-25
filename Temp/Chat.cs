using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Collabry
{
    public partial class Chat : Form
    {
        public User Owner {  get; set; }
        private User Selected {  get; set; }
        public Chat()
        {
            InitializeComponent();
        }
        public Chat(User u)
        {
            Owner = u;
            User _l = new User("_local", "Saved Messages", "", "", new Bitmap(1, 1), "");
            Owner.UserDM = new Dictionary<User, List<Message>> { 
                { _l, new List<Message>() { new Message(_l.UserTag, "-", DateTime.Now) } }
            };
            Owner.Connections = new Dictionary<User, TcpClient> {
                { _l, new TcpClient() }
            };
            Owner.UserDMSetting = new Dictionary<User, bool> {
                { _l, false}
            };
            Owner.UpdateStatic();
            ChatPanel.Visible = true;
            SettingPanel.Visible = false;
            MngCntPanel.Visible = false;
            InitializeComponent();
            foreach (var item in u.UserDM)
            {
                ContactsListBox.Items.Add((User)item.Key);
            }
            ContactsListBox.SelectedIndex = 0;
        }
        private void UpdateListBoxes()
        {
            MsgListBox.Items.Clear();
            foreach (Message m in Owner.UserDM[Selected])
            {
                MsgListBox.Items.Add(m);
            }
        }

        private void ContactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactsListBox.SelectedIndex == 0)
            {
                CallBtn.Enabled = false;
                CallBtn.Visible = false;
            }
            else
            {
                CallBtn.Enabled = true;
                CallBtn.Visible = true;
            }
            MsgListBox.Items.Clear();
            foreach (User item in ContactsListBox.Items)
            {
                if (sender.ToString().Split(':')[2].Contains(item.UserName))
                {
                    OtherUserName.Text = item.UserName;
                    Selected = item;
                    foreach (Message m in Owner.UserDM[item])
                    {
                        MsgListBox.Items.Add(m);
                    }
                }
            }
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"s:{sender} e:{e}");
            User.SendMessage(Owner.GetUserConnectionInfo(Selected), InputTxtBox.Text, false);
            Owner.UpdateVars();
            UpdateListBoxes();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChatPanel.Visible = false;
            SettingPanel.Visible = true;
            MngCntPanel.Visible = false;
            SettingPanel.Location = new Point(0, 27);
            SettingsLstBox.Items.Clear();
            foreach (var item in Owner.UserDM)
            {
                SettingsLstBox.Items.Add((User)item.Key);
            }
            SettingsLstBox.SelectedIndex = 0;
        }

        private void manageContactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChatPanel.Visible = false;
            SettingPanel.Visible = false;
            MngCntPanel.Visible = true;
            MngCntPanel.Location = new Point(0, 27);
        }

        private void CallBtn_Click(object sender, EventArgs e)
        {
            User.SendMessage(Owner.GetUserConnectionInfo(Selected), $"CALL>{Owner.UserTag}>IN", true);
            //MainForm call = new MainForm((User)Owner,(User)Selected, true);
            //call.Show();
        }

        private void SaveData_Click(object sender, EventArgs e)
        {
            Owner.UserTag = TagTxtBox.Text;
            Owner.UserName = NameTxtBox.Text;
            Owner.Email = LoginTxtBox.Text;
            Owner.Password = PassTxtBox.Text;
            Owner.UserInfo = AboutTxtBox.Text;
            Owner.SaveUserData();
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChatPanel.Visible = true;
            SettingPanel.Visible = false;
            MngCntPanel.Visible = false;
        }
    }
}
