using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Requests
{
    public class DeliveryOrderRequest : IRequest
    {
        public Guid UserUuid { get; set; }
        public Guid OrderUuid { get; set; }
    }
}
