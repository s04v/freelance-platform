﻿using Core.Orders.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Requests
{
    public class GetOrdersForCustomerRequest : IRequest<IEnumerable<Order>>
    {
        public Guid UserUuid { get; set; }
    }
}
