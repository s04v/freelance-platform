using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders
{
    public enum OrderStatus
    {
        New, 
        Delivered,
        InRevision,
        Accepted
    }
}
