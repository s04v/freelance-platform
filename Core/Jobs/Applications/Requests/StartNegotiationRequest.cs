using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications.Requests
{
    public class StartNegotiationRequest : IRequest
    {
        public Guid ApplicationUuid { get; set; }
        public Guid UserUuid { get; set; }
    }
}
