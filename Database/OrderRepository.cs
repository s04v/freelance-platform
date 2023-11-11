using Core.Jobs;
using Core.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class OrderRepository : IOrderRepository
    {
        public BaseDbContext _dbContext;

        public OrderRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Order order, CancellationToken token)
        {
            await _dbContext.Order.AddAsync(order, token);
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }
    }
}
