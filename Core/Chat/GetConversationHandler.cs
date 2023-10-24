using AutoMapper;
using Core.Common;
using Core.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class GetConversationHandler : IRequestHandler<GetConversationRequest, ConversationResponse>
    {
        public readonly IChatRepository _repository;
        public readonly IUserRepository _userRepository;

        public GetConversationHandler(IChatRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ConversationResponse> Handle(GetConversationRequest request, CancellationToken token)
        {
            var conversation = await _repository.GetConversationWithMessages(request.UserUuid, request.InterlocutorUuid, token)
                ?? throw new ErrorsException("Conversation not found");

            var interlocutorUuid = conversation.User1 == request.UserUuid ? conversation.User2 : conversation.User1;

            var interlocutor = await _userRepository.GetUser(o => o.Uuid == interlocutorUuid, token);

            var conversationResponse = new ConversationResponse
            {
                Uuid = conversation.Uuid,
                FirstName = interlocutor.FirstName,
                LastName = interlocutor.LastName,
                Messages = conversation.Messages
            };

            return conversationResponse;
        }
    }
}
