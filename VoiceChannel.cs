using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class VoiceChannel : ServerChannel
    {
        public int MaxUsers { get; set; } = 25;

        public string RelayIp { get; set; } = string.Empty;
        public int RelayPort { get; set; } = 0;
    }
}
