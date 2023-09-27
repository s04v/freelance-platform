using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class SendMessageRequest : IRequest<Message>
    {
        public Guid SenderUuid { get; set; }
        public Guid RecipientUuid { get; set; }
        public string Content { get; set; }
    }
}
