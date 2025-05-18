using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public abstract class ServerChannel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int ServerId { get; set; }

        [ForeignKey(nameof(ServerId))]
        public virtual Server Server { get; set; }
    }
}
