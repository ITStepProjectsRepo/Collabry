using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabry
{
    public class TextChannel : ServerChannel
    {
        public virtual List<Message_S> Messages_S { get; set; } = new List<Message_S>();
    }
}
