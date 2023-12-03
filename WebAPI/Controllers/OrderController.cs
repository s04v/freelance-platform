using Core.Orders;
using Core.Orders.Entities;
using Core.Orders.Requests;
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
        public async Task Create([FromBody] CreateOrderRequest request, CancellationToken token)
        {
            request.PerformerUuid = this.GetUserId().Value;

            await _mediator.Send(request, token);
        }

        [HttpGet("{id}")]
        public async Task<Order> GetOrder([FromRoute] Guid id, CancellationToken token)
        {
            var request = new GetOrderRequest
            {
                OrderUuid = id,
                UserUuid = this.GetUserId().Value,
            };

            return await _mediator.Send(request, token);
        }

        [HttpPost("{id}/Accept")]
        public async Task AcceptOrder([FromRoute] Guid id, CancellationToken token)
        {
            var request = new AcceptOrderRequest
            {
                OrderUuid = id,
                UserUuid = this.GetUserId().Value,
            };

            await _mediator.Send(request, token);
        }

        [HttpPost("{id}/Delivery")]
        public async Task DeliveryOrder([FromRoute] Guid id, CancellationToken token)
        {
            var request = new DeliveryOrderRequest
            {
                OrderUuid = id,
                UserUuid = this.GetUserId().Value
            };

            await _mediator.Send(request, token);
        }

        [HttpGet("Customer")]
        public async Task<IEnumerable<Order>> GetOrdersForCustomer(CancellationToken token)
        {
            var request = new GetOrdersForCustomerRequest
            {
                UserUuid = this.GetUserId().Value,
            };

            return await _mediator.Send(request, token);
        }


        [HttpGet("Performer")]
        public async Task<IEnumerable<Order>> GetOrdersForPerformer(CancellationToken token)
        {
            var request = new GetOrdersForPerformerRequest
            {
                UserUuid = this.GetUserId().Value,
            };

            return await _mediator.Send(request, token);
        }

    }
}
