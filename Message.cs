using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Message
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public DateTime SendTime { get; set; }
        //public File File { get; set; }

        public Message()
        {
            SendTime = DateTime.Now;
        }

        public Message(string sender, string text, DateTime sendTime)
        {
            Sender = sender;
            Text = text;
            SendTime = sendTime;
        }

        public void AddMessage(string sender, string text)
        {
            Message message = new Message(sender, text, DateTime.Now);
        }

        public void RemoveMessage(string sender, string text)
        {
            
        } 
    }
}