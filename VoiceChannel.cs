using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceChannel : ServerChannel
    {
        public List<User_S> ConnectedUsers { get; set; }
        public int MaxUsers { get; set; } = 25;
        public Dictionary<int, bool> MutedUsers { get; set; }
    }
}
