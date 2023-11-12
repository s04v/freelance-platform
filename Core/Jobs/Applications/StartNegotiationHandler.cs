using Core.Chat;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class StartNegotiationHandler : IRequestHandler<StartNegotiationRequest>
    {
        private readonly IJobRepository _repository;
        private readonly IChatRepository _chatRepository;

        public StartNegotiationHandler(IJobRepository repository, IChatRepository chatRepository)
        {
            _repository = repository;
            _chatRepository = chatRepository;
        }

        public async Task Handle(StartNegotiationRequest request, CancellationToken token)
        {
            var application = await _repository.GetApplication(o => o.Uuid == request.ApplicationUuid, token)
                ?? throw new ErrorsException("Application not found");

            application.Status = ApplicationStatus.Negotiation;

            await _repository.Save(token);

            var conversation= await _chatRepository.GetConversation(request.UserUuid, application.UserUuid, token);

            if (conversation == null)
            {
                var newConversation = new Conversation
                {
                    User1 = request.UserUuid,
                    User2 = application.UserUuid,
                };

                await _chatRepository.AddConversation(newConversation, token);
                await _chatRepository.Save(token);
            }
        }
    }
}
