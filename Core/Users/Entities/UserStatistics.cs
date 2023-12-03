using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users.Entities
{
    public class UserStatistics
    {
        public decimal EarnedMoney { get; set; }
        public decimal ApplicationCount { get; set; }
        public decimal DeliveredOrders { get; set; }
        public decimal SpentMoney { get; set; }
        public decimal PostedJobs { get; set; }
        public decimal CompletedOrders { get; set; }
    }
}
