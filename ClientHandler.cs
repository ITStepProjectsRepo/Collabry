using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using System.Windows;

namespace Collabry
{
    public abstract class ChatListItem { }
    public class DateHeaderItem : ChatListItem
    {
        public string DateText { get; set; }
    }

    public class OutgoingMessageItem : ChatListItem
    {
        public string Message { get; set; }
        public string Time { get; set; }
    }

    public class IncomingMessageItem : ChatListItem
    {
        public string Message { get; set; }
        public string Time { get; set; }
    }

    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DateHeaderTemplate { get; set; }
        public DataTemplate OutgoingMessageTemplate { get; set; }
        public DataTemplate IncomingMessageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //switch (item)
            //{
            //    case DateHeaderItem:
            //        return DateHeaderTemplate;
            //    case OutgoingMessageItem:
            //        return OutgoingMessageTemplate;
            //    case IncomingMessageItem:
            //        return IncomingMessageTemplate;
            //    default:
            //        return base.SelectTemplate(item, container);
            //}
            return base.SelectTemplate(item, container);
        }
    }

    public static class ClientHandler
    {
        private static readonly char[] charg =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`~!@№;$%^:&?*()-_=+<>,./\\{}[]".ToCharArray();
        private static readonly char[] chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789`~!@#№;$%^:&?*()-_=+<>,./\\{}[]".ToCharArray();

        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            var sb = new char[length];

            for (int i = 0; i < length; i++)
            {
                char c = charg[random.Next(chars.Length)];
                sb[i] = c;
            }

            return new string(sb);
        }
        
        public static string VigenereEncrypt(string input, string key)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char plainChar = input[i];
                char keyChar = key[i % key.Length];

                int plainIndex = Array.IndexOf(chars, plainChar);
                int keyIndex = Array.IndexOf(chars, keyChar);

                if (plainIndex == -1 || keyIndex == -1)
                    throw new ArgumentException("Input and key must contain only characters from the defined set.");

                int encryptedIndex = (plainIndex + keyIndex) % chars.Length;
                sb.Append(chars[encryptedIndex]);
            }

            return sb.ToString();
        }

        public static string VigenereDecrypt(string input, string key)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char cipherChar = input[i];
                char keyChar = key[i % key.Length];

                int cipherIndex = Array.IndexOf(chars, cipherChar);
                int keyIndex = Array.IndexOf(chars, keyChar);

                if (cipherIndex == -1 || keyIndex == -1)
                    throw new ArgumentException("Input and key must contain only characters from the defined set.");

                int decryptedIndex = (cipherIndex - keyIndex + chars.Length) % chars.Length;
                sb.Append(chars[decryptedIndex]);
            }

            return sb.ToString();
        }

        //private static void RecieveMessage(NetworkStream stream)
        //{
        //    try
        //    {
        //        StreamReader reader = new StreamReader(stream, Encoding.UTF8);

        //        while (stream.DataAvailable)
        //        {
        //            string message = reader.ReadLine();
        //            if (message == null)
        //                continue;

        //            if (message.StartsWith("SYS>"))
        //            {
        //                // SYSTEM messages
        //                try
        //                {
        //                    if (message.Split('>')[1].StartsWith("USERINFO"))
        //                    {
        //                        string data = message.Split('>')[1].Split(':')[1];
        //                        User newU = new User();
        //                        newU.UserTag = data.Split(';')[0];
        //                        newU.UserName = data.Split(';')[1];
        //                        newU.UserPicture = ClientHandler.ConvertToBitmap(data.Split(';')[2]);
        //                        newU.UserInfo = data.Split(';')[3];

        //                        Connections.Add(newU, stream);
        //                        UserDM.Add(newU, new List<Message>());
        //                        UserDMSetting.Add(newU, true);
        //                    }
        //                    else if (message.Split('>')[1].StartsWith("ASKINFO"))
        //                    {
        //                        StreamWriter sw = new StreamWriter(stream);
        //                        sw.WriteLine($"SYS>{UserTag}>USERINFO:{UserTag};{UserName};{ClientHandler.ConvertToString(UserPicture)};{UserInfo}");
        //                        stream.Flush();
        //                    }
        //                    else { }
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"HandleSystemMessage error: {ex.Message}");
        //                }
        //            }
        //            else if (message.StartsWith("PAK>")) { throw new NotImplementedException(); }
        //            else if (message.StartsWith("MESS>"))
        //            {
        //                foreach (var value in Connections)
        //                {
        //                    if (Connections.Values.Equals(value.Value))
        //                    {
        //                        // Handle normal messages
        //                        User connectionKey = value.Key;

        //                        UserDM[connectionKey].Add(new Message(connectionKey.UserTag, message.Split('|')[0],
        //                            new DateTime(
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[0]),
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[1]),
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[2]),
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[3]),
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[4]),
        //                                Convert.ToInt32(message.Split('|')[1].Split('.')[5]))));

        //                        Debug.WriteLine($"[DM] {connectionKey}: {message}");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"ReceiveMessage error: {ex.Message}");
        //    }
        //}

        public static object ConvertToString(Bitmap img)
        {
            return "0";
        }

        internal static Bitmap ConvertToBitmap(string v)
        {
            if (v == null || v == "0")
                return null;
            return new Bitmap(1, 1);
        }
    }
}
