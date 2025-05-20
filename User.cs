using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Shapes;


namespace Collabry
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        private static string UserTag_S { get; set; }
        public static Dictionary<User, TcpClient> ConnectionsS { get; set; }
        public static Dictionary<User, List<Message>> UserDM_S { get; set; }
        public static Dictionary<User, bool> UserDMSetting_S { get; set; }
        public Dictionary<User, TcpClient> Connections { get; set; }
        public Dictionary<User, List<Message>> UserDM { get; set; }
        public Dictionary<User, bool> UserDMSetting { get; set; }
        public string UserTag { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] UserPictureData { get; set; }

        [NotMapped]
        public Bitmap UserPicture
        {
            get
            {
                if (UserPictureData == null) return null;
                using (var ms = new MemoryStream(UserPictureData))
                {
                    return new Bitmap(ms);
                }
            }
            set
            {
                if (value == null)
                {
                    UserPictureData = null;
                    return;
                }

                using (var ms = new MemoryStream())
                {
                    value.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    UserPictureData = ms.ToArray();
                }
            }
        }
        public string UserInfo { get; set; }

        public override string ToString()
        {
            return $"@{UserTag} {UserName}" +
            $"About: {UserInfo}";
        }
        public static User GetUserData(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                if (System.IO.File.Exists(filename))
                {
                    string[] data = System.IO.File.ReadAllLines(filename, Encoding.UTF8);
                    var _u = new User(data[6], data[7], data[2], data[3], new Bitmap(1,1), data[8]);
                    return _u;
                }
            }
            return null;
        }
        public void SaveUserData()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Data\\Users"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Data\\Users");
            }
            if (Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\Users").Length>0)
            {
                foreach (string line in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\Users"))
                {
                    var filestream = new FileStream(line, FileMode.Open);
                    var file = new StreamReader(filestream, Encoding.UTF8, true, 256);
                    file.ReadLine();
                    file.ReadLine();
                    string[] lineOfText = new string[] { file.ReadLine(), file.ReadLine() };
                    file.Close();
                    if (lineOfText[0] == Email)
                    {
                        if (lineOfText[1] == Password)
                        {
                            System.IO.File.WriteAllLines(line, new string[] {
                                $"[",
                                $"[",
                                $"{Email}",
                                $"{Password}",
                                $"]",
                                $"[",
                                $"{UserTag}",
                                $"{UserName}",
                                $"{UserInfo}",
                                $"]",
                                $"]"
                            });
                        }
                    }
                }
            }
            else
            {
                System.IO.File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Data\\Users\\" + (Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Data\\Users").Length+1) +".txt", new string[] {
                    $"[",
                    $"[",
                    $"{Email}",
                    $"{Password}",
                    $"]",
                    $"[",
                    $"{UserTag}",
                    $"{UserName}",
                    $"{UserInfo}",
                    $"]",
                    $"]"
                });
            }
        }

        public void NewForm()
        {
            Thread t = new Thread(() => {
                var f = new ShowUsrInfo(UserTag, UserName,
                Email, Password,
                UserPicture, UserInfo,
                ConnectionsS,
                UserDMSetting_S);
                f.ShowDialog();
            });
            t.Start();
        }

        public User() { }
        public User(string userTag, string userName, string email, string password, Bitmap userPicture, string userInfo)
        {
            UserTag = userTag;
            UserName = userName;
            Email = email;
            Password = password;
            UserPicture = userPicture;
            UserInfo = userInfo;
            UserTag_S = userTag;
        }
        public string MakeInvitation()
        {
            string cipherKey = ClientHandler.GenerateRandomString(8);
            string header = string.Join("//", new[] { "https:", "invite.clbry" });
            string invite = string.Join("||", new[] { $"{header}",$"{ClientHandler.VigenereEncrypt("192.168.1.1:4870", cipherKey)}" });
            invite = string.Join("#", new[] { $"{invite}", $"{cipherKey}" });
            return invite;
        }

        public void AddDM(string invite)
        {
            try
            {
                // Decrypt the invite string (e.g., using Vigenère cipher)
                invite = invite.Split(new[] { '|', '|' })[2];
                string cipherKey = invite.Split('#')[1]; 
                string invite_d = ClientHandler.VigenereDecrypt(invite.Split('#')[0], cipherKey);

                // Expected format after decryption: "ip:port"
                string[] parts = invite_d.Split(':');
                if (parts.Length != 2)
                    throw new ArgumentException("Invalid invite format after decryption.");

                string ip = parts[0];
                int port = int.Parse(parts[1]);

                Debug.WriteLine($"REQUESTING USERINFO FROM {ip}:{port}");
                TcpClient client = new TcpClient();
                client.Connect(ip, port);

                string connectionKey = $"{ip}:{port}";
                //Connections[connectionKey] = client;

                // Start thread to receive messages
                Thread thread = new Thread(() => ReceiveMessage(client));
                thread.IsBackground = true;
                thread.Start();

                // Send system message
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                writer.WriteLine($"SYS>{UserTag}>ASKINFO");
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddDM failed: {ex.Message}");
            }
        }

        public static void SendMessage(TcpClient client, string message)
        {
            try
            {
                if (client == null || !client.Connected)
                {
                    Debug.WriteLine("SendMessage failed: Client is not connected.");
                    return;
                }

                NetworkStream stream = client.GetStream();
                if (!stream.CanWrite)
                {
                    Debug.WriteLine("SendMessage failed: Cannot write to stream.");
                    return;
                }

                using (var writer = new StreamWriter(stream))
                {
                    // Message used from class, it`s format:
                    // MESS>{TAG}>{MESSAGE}|{DATETIME.YYYY}.{DATETIME.MM}.{DATETIME.DD}.{DATETIME.H}.{DATETIME.M}.{DATETIME.S}
                    Message _msg = new Message() { Sender = UserTag_S, Text = message, SendTime = DateTime.Now };
                    writer.AutoFlush = true;
                    writer.WriteLine(
                        $"MESS>{UserTag_S}>{_msg.Text}|" +
                        $"{_msg.SendTime.Year}." +
                        $"{_msg.SendTime.Month}." +
                        $"{_msg.SendTime.Day}." +
                        $"{_msg.SendTime.Hour}." +
                        $"{_msg.SendTime.Minute}." +
                        $"{_msg.SendTime.Second}");
                    writer.Flush();

                    AddMessageDM(client, _msg.Text, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendMessage error: {ex.Message}");
            }
        }

        private void ReceiveMessage(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                while (client.Connected)
                {
                    // Message used from class, it`s format:
                    // [TYPE]>{TAG}>{MESSAGE}
                    string message = reader.ReadLine();
                    if (message == null)
                        continue;

                    if (message.StartsWith("SYS>"))
                    {
                        // Sytem messages
                        try
                        {
                            if (message.Split('>')[1].StartsWith("USERINFO"))
                            {
                                string data = message.Split('>')[1].Split(':')[1];
                                User newU = new User();
                                newU.UserTag = data.Split(';')[0];
                                newU.UserName = data.Split(';')[1];
                                newU.UserPicture = ClientHandler.ConvertToBitmap(data.Split(';')[2]);
                                newU.UserInfo = data.Split(';')[3];

                                ConnectionsS.Add(newU, client);
                                UserDM_S.Add(newU, new List<Message>());
                                UserDMSetting_S.Add(newU, true);  
                            }
                            else if (message.Split('>')[1].StartsWith("ASKINFO"))
                            {
                                StreamWriter sw = new StreamWriter(stream);
                                sw.WriteLine($"SYS>{UserTag}>USERINFO:{UserTag};{UserName};{ClientHandler.ConvertToString(UserPicture)};{UserInfo}");
                                stream.Flush();
                            }
                            else { }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"HandleSystemMessage error: {ex.Message}");
                        }
                    }
                    else if (message.StartsWith("PAK>")) { throw new NotImplementedException(); }
                    else if (message.StartsWith("MESS>"))
                    {
                        // Message used from class, it`s format:
                        // MESS>{TAG}>{MESSAGE}|{DATETIME.YYYY}.{DATETIME.MM}.{DATETIME.DD}.{DATETIME.H}.{DATETIME.M}.{DATETIME.S}
                        foreach (var value in ConnectionsS)
                        {
                            if (ConnectionsS.Values.Equals(client))
                            {
                                AddMessageDM(value.Key, message, false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReceiveMessage error: {ex.Message}");
            }
        }

        private static void AddMessageDM(User user, string message, bool isSelf)
        {
            try
            {
                DateTime sendTime = DateTime.Now;
                if (!isSelf)
                {
                    User connectionKey = user;
                    UserDM_S[connectionKey].Add(new Message(connectionKey.UserTag, message.Split('|')[0],
                                    new DateTime(
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[0]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[1]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[2]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[3]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[4]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[5]))));

                    Debug.WriteLine($"[DM] {connectionKey.UserTag}: {message}");
                }
                else
                {
                    User connectionKey = user;
                    UserDM_S[connectionKey].Add(new Message(UserTag_S, message, sendTime));

                    Debug.WriteLine($"[DM] {connectionKey.UserTag}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddMessageDM error: {ex.Message}");
            }
        }

        private static void AddMessageDM(TcpClient user, string message, bool isSelf)
        {
            try
            {
                DateTime sendTime = DateTime.Now;
                if (!isSelf)
                {
                    foreach (var k in ConnectionsS)
                    {
                        if (k.Value.Equals(user))
                        {
                            User connectionKey = k.Key;
                            UserDM_S[connectionKey].Add(new Message(connectionKey.UserTag, message.Split('|')[0],
                                            new DateTime(
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[0]),
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[1]),
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[2]),
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[3]),
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[4]),
                                                Convert.ToInt32(message.Split('|')[1].Split('.')[5]))));

                            Debug.WriteLine($"[DM] {connectionKey.UserTag}: {message}");
                        }
                    }
                }
                else
                {
                    foreach (var k in ConnectionsS)
                    {
                        if (k.Value.Equals(user))
                        {
                            User connectionKey = k.Key;
                            UserDM_S[connectionKey].Add(new Message(UserTag_S, message, sendTime));

                            Debug.WriteLine($"[DM] {connectionKey.UserTag}: {message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddMessageDM error: {ex.Message}");
            }
        }
    }
}
