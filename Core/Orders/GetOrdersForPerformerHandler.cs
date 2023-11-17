using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class GetOrdersForPerformerHandler : IRequestHandler<GetOrdersForPerformerRequest, IEnumerable<Order>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersForPerformerHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersForPerformerRequest request, CancellationToken token)
        {
            var orders = await _repository.GetOrdersByPerformerUuid(request.UserUuid, token);

            return orders;
        }
    }
}
