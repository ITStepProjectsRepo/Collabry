using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Collabry
{
    public class User
    {
        public string UserTag { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public BitmapImage UserPicture { get; set; }
        public string UserInfo { get; set; }
        public Dictionary<User, NetworkStream> Connections { get; set; }
        public Dictionary<User, List<Message>> UserDM { get; set; }
        public Dictionary<User, bool> UserDMSetting { get; set; }
        public User() { }
        public User(string userTag, string userName, string email, string password, BitmapImage userPicture, string userInfo)
        {
            UserTag = userTag;
            UserName = userName;
            Email = email;
            Password = password;
            UserPicture = userPicture;
            UserInfo = userInfo;
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
                    writer.AutoFlush = true;
                    writer.WriteLine(message);
                    writer.Flush();
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
                    string message = reader.ReadLine();
                    if (message == null)
                        continue;

                    if (message.StartsWith("SYS>"))
                    {
                        // Example: SYS>JohnDoe>INFO:John Doe;john@example.com
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

                                Connections.Add(newU, stream);
                                UserDM.Add(newU, new List<Message>());
                                UserDMSetting.Add(newU, true);  
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
                        foreach (var value in Connections)
                        {
                            if (Connections.Values.Equals(value.Value))
                            {
                                // Handle normal messages
                                User connectionKey = value.Key;

                                UserDM[connectionKey].Add(new Message(connectionKey.UserTag, message.Split('|')[0],
                                    new DateTime(
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[0]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[1]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[2]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[3]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[4]),
                                        Convert.ToInt32(message.Split('|')[1].Split('.')[5]))));

                                Debug.WriteLine($"[DM] {connectionKey}: {message}");
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
    }
}
