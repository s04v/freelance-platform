using Core.Common;
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
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest>
    {
        private IOrderRepository _repository;

        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderRequest request, CancellationToken token)
        {
            var order = new Order
            {
                Title = request.Title,
                Price = request.Price,
                DeliveryDays = request.DeliveryDays,
                CustomerUuid = request.CustomerUuid,
                PerformerUuid = request.PerformerUuid,
                Status = OrderStatus.New,
                CreatedDate = DateTime.Now,
            };

            await _repository.Create(order, token);

            await _repository.Save(token);
        }
    }
}
