using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class RejectApplicationRequest : IRequest
    {
        public Guid ApplicationUuid { get; set; }
        public Guid UserUuid { get; set; }
    }
}
