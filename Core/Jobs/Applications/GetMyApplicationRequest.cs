using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class GetMyApplicationRequest : IRequest<MyApplicationResponse>
    {
        public Guid UserUuid { get; set; }
        public Guid JobUuid { get; set; }
    }
}
