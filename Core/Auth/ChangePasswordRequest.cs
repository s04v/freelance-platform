using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class ChangePasswordRequest : IRequest<Unit>
    {
        public Guid? UserUuid { get; set; }
        public string? RecoveryToken { get; set; } 
        public string NewPassword { get; set; }
    }
}
