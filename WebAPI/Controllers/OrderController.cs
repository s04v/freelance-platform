using Core.Orders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async void Create([FromBody] CreateOrderRequest request, CancellationToken token)
        {
            request.CustomerUuid = this.GetUserId().Value;

            await _mediator.Send(request, token);
        }
    }
}
