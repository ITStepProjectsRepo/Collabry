using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Collabry
{
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

        public static object ConvertToString(BitmapImage img)
        {
            return "0";
        }

        internal static BitmapImage ConvertToBitmap(string v)
        {
            if (v == null || v == "0")
                return null;
            return new BitmapImage();
        }
    }
}
