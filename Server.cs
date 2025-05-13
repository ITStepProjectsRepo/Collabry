using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Server
    {
        public int Id { get; set; }
        public string ServerName { get; set; }
        public List<User> ServerMembers { get; set; }
        public List<ServerRole> ServerRoles { get; set; }
        public List<ServerChannel> ServerChannels { get; set; }
        public List<ServerGroup> ServerGroups { get; set; }

        public Server()
        {
            ServerMembers = new List<User>();
            ServerRoles = new List<ServerRole>();
            ServerChannels = new List<ServerChannel>();
            ServerGroups = new List<ServerGroup>();
        }
    }
}
