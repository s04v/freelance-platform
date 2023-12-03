using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat.Entities
{
    public class ConversationItem
    {
        public Guid Uuid { get; set; }
        public Guid UserUuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Message LastMessage { get; set; }
    }
}
