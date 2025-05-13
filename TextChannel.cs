using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class TextChannel : ServerChannel
    {
        public List<Message> Messages { get; set; }
        public NotificationSettings NotificationSettings { get; set; }
    }
}
