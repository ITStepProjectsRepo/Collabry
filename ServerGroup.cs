using System;
using System.Collections.Generic;

namespace Collabry
{
    public class ServerGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<ServerChat> ServerChats { get; set; }
    }
}