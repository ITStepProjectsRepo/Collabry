using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class ServerChat
    {
        public int Id { get; set; }
        public string ChatName { get; set; }
        public int ChatType { get; set; }
        public int NotificationSettings { get; set; }

        public List<Message> Messages { get; set; }
    }
}
