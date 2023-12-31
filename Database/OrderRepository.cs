﻿using Core.Jobs;
using Core.Jobs.Entities;
using Core.Orders;
using Core.Orders.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Order>> GetOrders(Expression<Func<Order, bool>> predicate, CancellationToken token)
        {
            return await _dbContext.Order
                .Where(predicate)
                .ToListAsync(token);
        }

        public async Task Create(Order order, CancellationToken token)
        {
            await _dbContext.Order.AddAsync(order, token);
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task<Order> GetOrderByUuid(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
                .Where(o => o.Uuid == uuid)
                .FirstAsync(token);
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerUuid(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
                .Where(o => o.CustomerUuid == uuid)
                .ToListAsync(token);
        }
        public async Task<IEnumerable<Order>> GetOrdersByPerformerUuid(Guid uuid, CancellationToken token) 
        {
            return await _dbContext.Order
                .Where(o => o.PerformerUuid == uuid)
                .ToListAsync(token);
        }

        public async Task<decimal> GetEarnedMoneyOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
               .Where(o => o.PerformerUuid == uuid && o.Status == Core.Common.OrderStatus.Delivered)
               .SumAsync(o => o.Price);
        }

        public async Task<decimal> GetSpentMoneyOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
               .Where(o => o.CustomerUuid == uuid && o.Status == Core.Common.OrderStatus.Delivered)
               .SumAsync(o => o.Price);
        }

        public async Task<int> GetDeliveredOrdersCountOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
               .Where(o => o.PerformerUuid == uuid && o.Status == Core.Common.OrderStatus.Delivered)
               .CountAsync();
        }
        public async Task<int> GetCompletedOrdersCountOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Order
               .Where(o => o.CustomerUuid == uuid && o.Status == Core.Common.OrderStatus.Delivered)
               .CountAsync();
        }
    }
}
