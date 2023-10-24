using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class GetConversationRequest : IRequest<ConversationResponse>
    {
        public Guid UserUuid { get; set; }
        public Guid InterlocutorUuid { get; set; }
    }
}
