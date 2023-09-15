using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetMyJobsRequest : IRequest<IEnumerable<MyJobItem>>
    {
        public Guid UserUuid { get; set; }
    }
}
