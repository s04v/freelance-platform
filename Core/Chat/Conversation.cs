using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class Conversation
    {
        public Guid Uuid { get; set; }
        public Guid User1 { get; set; }
        public Guid User2 { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
