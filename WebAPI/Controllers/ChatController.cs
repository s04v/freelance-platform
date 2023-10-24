using Core.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WebAPI.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/Chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IChatRepository _repository;
        public ChatController(IMediator mediator, IChatRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ConversationResponse> GetConversation(
            [FromRoute] Guid id,
            CancellationToken token)
        {

            var request = new GetConversationRequest
            {
                UserUuid = this.GetUserId().Value,
                InterlocutorUuid = id,
            };

            return await _mediator.Send(request, token);
        }

        [HttpGet]
        public async Task<IEnumerable<ConversationItem>> GetConversations(CancellationToken token)
        {

            return await _repository.GetMyConversations(this.GetUserId().Value, token);
        }
    }
}
