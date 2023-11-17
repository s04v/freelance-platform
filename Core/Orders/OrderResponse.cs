using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class OrderResponse
    {
        public Guid Uuid { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int DeliveryDays { get; set; }
        public Guid CustomerUuid { get; set; }
        public Guid PerformerUuid { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
