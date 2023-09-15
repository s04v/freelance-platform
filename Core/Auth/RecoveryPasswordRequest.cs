﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class RecoveryPasswordRequest : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
