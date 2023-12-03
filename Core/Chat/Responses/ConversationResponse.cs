using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Chat.Entities;

namespace Core.Chat
{
    public class ConversationResponse
    {
        public Guid Uuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
