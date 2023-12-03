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
    public class DeliveryOrderHandler : IRequestHandler<DeliveryOrderRequest>
    {
        private readonly IOrderRepository _orderRepository;

        public DeliveryOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(DeliveryOrderRequest request, CancellationToken token)
        {
            var order = await _orderRepository.GetOrderByUuid(request.OrderUuid, token);

            if (order.PerformerUuid != request.UserUuid)
            {
                throw new ErrorsException("Denied access");
            }

            order.Status = OrderStatus.Delivered;

            await _orderRepository.Save(token);
        }
    }
}
