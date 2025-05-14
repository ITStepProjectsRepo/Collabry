using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Message_S : Message
    {
        public int TextChannelId { get; set; }

        [ForeignKey(nameof(TextChannelId))]
        public virtual TextChannel TextChannel { get; set; }
    }
}
