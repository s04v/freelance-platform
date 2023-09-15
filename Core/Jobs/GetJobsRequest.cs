using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs 
{
    public class GetJobsRequest : IRequest<JobListResponse>
    {
        public int PageNumber { get; set; }
    }
}
