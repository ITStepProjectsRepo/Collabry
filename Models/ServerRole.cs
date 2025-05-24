using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Collabry
{
    public class ServerRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public Color RoleColor { get; set; }

        public virtual List<User_S> Users_S { get; set; } = new List<User_S>();

        public bool CanManageMessages { get; set; }
        public bool CanKickUsers { get; set; }
        public bool CanBanUsers { get; set; }
        public bool CanCreateChannels { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString() => $"{RoleName}";
    }
}