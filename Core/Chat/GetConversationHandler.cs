using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class GetConversationHandler : IRequestHandler<GetConversationRequest, Conversation>
    {
        public readonly IChatRepository _repository;

        public GetConversationHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Conversation> Handle(GetConversationRequest request, CancellationToken token)
        {
            var conversation = await _repository.GetConversationWithMessages(request.UserUuid, request.InterlocutorUuid, token)
                ?? throw new ErrorsException("Conversation not found");

            return conversation;
        }
    }
}
