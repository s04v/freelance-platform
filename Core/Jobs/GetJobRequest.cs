using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetJobRequest : IRequest<JobResponseItem>
    {
        public Guid? UserUuid { get; set; }
        public Guid JobUuid { get; set; }
    }
}
