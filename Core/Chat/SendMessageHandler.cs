using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Chat
{
    public class SendMessageHandler : IRequestHandler<SendMessageRequest, Message>
    {
        private readonly IChatRepository _chatRepository;

        public SendMessageHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Message> Handle(SendMessageRequest request, CancellationToken token)
        {
            var conversation = await _chatRepository.GetConversation(request.SenderUuid, request.RecipientUuid, token);

            if (conversation == null)
            {
                conversation = new Conversation
                {
                    User1 = request.SenderUuid,
                    User2 = request.RecipientUuid
                };

                await _chatRepository.AddConversation(conversation, token);
                await _chatRepository.Save(token);
            }

            var message = new Message
            {
                SenderUuid = request.SenderUuid,
                ConversationUuid = conversation.Uuid,
                Content = request.Content,
                SendDate = DateTime.Now,
            };

            await _chatRepository.AddMessage(message, token);
            await _chatRepository.Save(token);

            return message;
        }
    }
}
