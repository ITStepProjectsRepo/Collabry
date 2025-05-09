using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Servers
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public List<ServerChat> ServerChats { get; set; }
    }
}
