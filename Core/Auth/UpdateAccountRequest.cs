using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class UpdateAccountRequest : IRequest
    {
        public Guid UserUuid { get; set; }
        public string Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
