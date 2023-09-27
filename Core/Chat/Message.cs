using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class Message
    {
        public Guid Uuid { get; set; }
        public Guid SenderUuid { get; set; }
        public Guid ConversationUuid { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        
        public Conversation? Conversation { get; set; } = null;
    }
}
