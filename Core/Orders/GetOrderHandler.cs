using Core.Orders.Entities;
using Core.Orders.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class GetOrderHandler : IRequestHandler<GetOrderRequest, Order>
    {
        private readonly IOrderRepository _repository;

        public GetOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetOrderRequest request, CancellationToken token)
        {
            var order = await _repository.GetOrderByUuid(request.OrderUuid, token);

            return order;
        }
    }
}
