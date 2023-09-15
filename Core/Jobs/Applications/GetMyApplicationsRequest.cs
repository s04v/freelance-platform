using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class GetMyApplicationsRequest : IRequest<IEnumerable<MyApplicationWithJob>>
    {
        public Guid UserUuid { get; set; } 
    }
}
