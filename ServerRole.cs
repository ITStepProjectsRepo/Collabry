using System;
using System.Windows.Documents;
using System.Collections.Generic;

namespace Collabry
{
    public class ServerRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}