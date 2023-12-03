using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Jobs.Entities;
using Core.Orders.Entities;

namespace Core.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders(Expression<Func<Order, bool>> predicate, CancellationToken token);
        Task Create(Order order, CancellationToken token);
        Task Save(CancellationToken token);
        Task<Order> GetOrderByUuid(Guid uuid, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByCustomerUuid(Guid uuid, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByPerformerUuid(Guid uuid, CancellationToken token);

        Task<decimal> GetEarnedMoneyOfUser(Guid uuid, CancellationToken token);
        Task<decimal> GetSpentMoneyOfUser(Guid uuid, CancellationToken token);
        Task<int> GetDeliveredOrdersCountOfUser(Guid uuid, CancellationToken token);
        Task<int> GetCompletedOrdersCountOfUser(Guid uuid, CancellationToken token);

    }
}
