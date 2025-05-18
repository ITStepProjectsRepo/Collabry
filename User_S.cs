using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class User_S
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int ServerId { get; set; }

        [ForeignKey(nameof(ServerId))]
        public virtual Server Server { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        public bool IsMicrophoneOn { get; private set; } = true;

        public void TurnMicOff()
        {
            IsMicrophoneOn = false;
            Sender?.Stop();
        }

        public void TurnMicOn()
        {
            if (!IsMuted)
            {
                IsMicrophoneOn = true;
                Sender?.Start();
            }
        }

        public bool IsMuted { get; set; } = false;
        public void Mute()
        {
            IsMuted = true;
            Sender?.Stop();
        }

        public void Unmute()
        {
            IsMuted = false;
            if (IsMicrophoneOn)
                Sender?.Start();
        }
        public bool IsBanned { get; set; } = false;

        public VoiceChatSender Sender { get; set; }
        public VoiceChatReceiver Receiver { get; set; }

        public virtual List<ServerRole> ServerRoles { get; set; } = new List<ServerRole>();
    }
}
