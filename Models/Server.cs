using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class Server
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<User_S> ServerMembers { get; set; } = new List<User_S>();
        public virtual List<ServerRole> ServerRoles { get; set; } = new List<ServerRole>();
        public virtual List<ServerChannel> ServerChannels { get; set; } = new List<ServerChannel>();
        public virtual List<ServerGroup> ServerGroups { get; set; } = new List<ServerGroup>();
    }
}
