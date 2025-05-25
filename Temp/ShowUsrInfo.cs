using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Collabry
{
    public partial class ShowUsrInfo : Form
    {
        private string UserTag { get; set; }
        private string UserName { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private Bitmap UserPicture { get; set; }
        private string UserInfo { get; set; }
        private Dictionary<User, TcpClient> ConnectionS { get; set; }
        private Dictionary<User, bool> UserDMSettingS { get; set; }

        public ShowUsrInfo()
        {
            InitializeComponent();
        }

        public ShowUsrInfo(string userTag, string userName, 
            string email, string password, 
            Bitmap userPicture, string userInfo,
            Dictionary<User, TcpClient> connectionS,
            Dictionary<User, bool> userDMSettingS)
        {
            UserTag = userTag;
            UserName = userName;
            Email = email;
            Password = password;
            UserPicture = userPicture;
            UserInfo = userInfo;
            ConnectionS = connectionS;
            UserDMSettingS = userDMSettingS;
            InitializeComponent();
            if (connectionS != null && UserDMSettingS != null)
            {
                foreach (var item in UserDMSettingS)
                {
                    DMS_LB.Items.Add(item.Key, item.Value);
                }
            }
            else
            {
                DMS_LB.Items.Add($"LocalServer _", true);
            }
            
            DMS_LB.Update();

            TagL.Text = UserTag;
            NameL.Text = UserName;
            EmailL.Text = Email;
            PassL.Text = Password;
            AboutL.Text = UserInfo;
            PicPB.Image = UserPicture;
        }
    }
}
