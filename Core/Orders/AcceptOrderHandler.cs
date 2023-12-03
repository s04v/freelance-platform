using Core.Common;
using Core.Orders.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class AcceptOrderHandler : IRequestHandler<AcceptOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;

        public AcceptOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(AcceptOrderRequest request, CancellationToken token)
        {
            var order = await _orderRepository.GetOrderByUuid(request.OrderUuid, token);

            if (order.CustomerUuid != request.UserUuid)
            {
                throw new ErrorsException("Denied access");
            }

            order.Status = OrderStatus.Accepted;

            await _orderRepository.Save(token);
        }
    }
}
