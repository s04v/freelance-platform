using Core.Chat;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            var id = GetUserId();

            Groups.AddToGroupAsync(Context.ConnectionId, id);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var id = GetUserId();

            Groups.RemoveFromGroupAsync(Context.ConnectionId, id);

            return base.OnDisconnectedAsync(exception);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Send(string message, string recipient)
        {
            var id = GetUserId();

            var sendMessageRequest = new SendMessageRequest
            {
                Content = message,
                RecipientUuid = Guid.Parse(recipient),
                SenderUuid = Guid.Parse(id),
            };

            var newMessage = await _mediator.Send(sendMessageRequest);


            await Clients.Group(recipient).SendAsync("NewMessage", newMessage);
        }

        private string? GetUserId()
        {
           return Context.User?.Claims?.FirstOrDefault(o => o.Type == "id")?.Value;
        }
    }
}
