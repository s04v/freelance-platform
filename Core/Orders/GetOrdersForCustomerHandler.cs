using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class GetOrdersForCustomerHandler : IRequestHandler<GetOrdersForCustomerRequest, IEnumerable<Order>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersForCustomerHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersForCustomerRequest request, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByCustomerUuid(request.UserUuid, token);

            return orders;
        }
    }
}
