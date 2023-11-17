﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public class GetOrderRequest : IRequest<Order>
    {
        public Guid OrderUuid { get; set; }
        public Guid UserUuid { get; set; }
    }
}
