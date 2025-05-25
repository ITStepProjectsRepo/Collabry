using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collabry
{
    public partial class Login : Form
    {
        private readonly string UserData = Directory.GetCurrentDirectory() + "\\Data\\Users";
        public Login()
        {
            InitializeComponent();
            regPan.Location = new Point(12,12);
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            //search user in database and load everything about him
            //we`ll be using pressets
            LogUserIn(SignLogin.Text, SignPass.Text);
        }

        private void LogUserIn(string login, string pass)
        {
            if (Directory.GetFiles(UserData).Length > 0)
            {
                foreach (string line in Directory.GetFiles(UserData))
                {
                    var filestream = new FileStream(line,FileMode.Open);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 256);
                    file.ReadLine();
                    file.ReadLine();
                    string[] lineOfText = new string[] { file.ReadLine(), file.ReadLine() };
                    file.Close();
                    if (lineOfText[0] == login)
                    {
                        if (lineOfText[1] == pass)
                        {
                            MainWindow.Device_S = User.GetUserData(line);
                            DialogResult = DialogResult.OK;
                            break;
                        }
                        else
                            MessageBox.Show("Check inputed data for mistakes", "Incorrect Data Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Check inputed data for mistakes", "Incorrect Data Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void signinBtn_Click(object sender, EventArgs e)
        {
            logPan.Visible = false;
            regPan.Visible = true;
        }

        private void RegSignIn_Click(object sender, EventArgs e)
        {
            if (politicsChkBox.Checked)
            {
                if (string.IsNullOrEmpty(TagTxtBox.Text) ||
                    string.IsNullOrEmpty(NameTxtBox.Text) ||
                    string.IsNullOrEmpty(LoginTxtBox.Text) ||
                    string.IsNullOrEmpty(PassTxtBox.Text))
                {
                    MessageBox.Show("Null parameter cant be processed!", "Null Parameter found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (TagTxtBox.Text.Length < 6 || NameTxtBox.Text.Length < 6 || LoginTxtBox.Text.Length < 6 || PassTxtBox.Text.Length < 6)
                {
                    MessageBox.Show("Data in fields contains not enough symbols for minimum reqirement", "Minimum size Reqirement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MainWindow.Device_S = new User(TagTxtBox.Text, NameTxtBox.Text, LoginTxtBox.Text, PassTxtBox.Text, new Bitmap(1, 1), "");
                    DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void politicsChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (politicsChkBox.Checked)
            {
                RegSignIn.Enabled = true;
            }
            else
            {
                RegSignIn.Enabled = false;
            }
        }
    }
}
