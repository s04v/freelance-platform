using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class GetMeRequest : IRequest<User>
    {
        public Guid UserUuid { get; set; }
    }
}
