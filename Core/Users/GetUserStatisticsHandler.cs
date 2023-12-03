using Core.Common;
using Core.Jobs;
using Core.Orders;
using Core.Users.Entities;
using Core.Users.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class GetUserStatisticsHandler : IRequestHandler<GetUserStatisticsRequest, UserStatistics>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IJobRepository _jobRepository;

        public GetUserStatisticsHandler(IJobRepository jobRepository, IOrderRepository orderRepository)
        {
            _jobRepository = jobRepository;
            _orderRepository = orderRepository;
        }

        public async Task<UserStatistics> Handle(GetUserStatisticsRequest request, CancellationToken token)
        {
            var userUuid = request.UserUuid;

            var earnedMoney = await _orderRepository.GetEarnedMoneyOfUser(userUuid, token);
            var deliveredOrdersCount = await _orderRepository.GetDeliveredOrdersCountOfUser(userUuid, token);
            var spentMoney = await _orderRepository.GetSpentMoneyOfUser(userUuid, token);
            var completedOrders = await _orderRepository.GetCompletedOrdersCountOfUser(userUuid, token);
            var applicationCount = await _jobRepository.GetApplicationCountOfUser(userUuid, token);
            var postedJobs = await _jobRepository.GetJobCountOfUser(userUuid, token);

            var statistics = new UserStatistics
            {
                EarnedMoney = earnedMoney,
                DeliveredOrders = deliveredOrdersCount,
                SpentMoney = spentMoney,
                CompletedOrders = completedOrders,
                ApplicationCount = applicationCount,
                PostedJobs = postedJobs
            };

            return statistics;
        }
    }
}
