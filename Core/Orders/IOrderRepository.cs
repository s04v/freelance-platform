using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public interface IOrderRepository
    {
        Task Create(Order order, CancellationToken token);
        Task Save(CancellationToken token);
        Task<Order> GetOrderByUuid(Guid uuid, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByCustomerUuid(Guid uuid, CancellationToken token);
        Task<IEnumerable<Order>> GetOrdersByPerformerUuid(Guid uuid, CancellationToken token);
    }
}
