using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Sender { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime SendTime { get; set; }

        public bool IsEdited { get; set; } = false;

        //public File File { get; set; }
        public override string ToString()
        {
            return $"{Sender} [{SendTime}]: {Text}";
        }
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
    }
}