﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth.Requests
{
    public class ChangePasswordRequest : IRequest<Unit>
    {
        public string? RecoveryToken { get; set; }
        public string NewPassword { get; set; }
    }
}
