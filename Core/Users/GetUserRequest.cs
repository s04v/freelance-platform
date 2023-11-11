﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class GetUserRequest : IRequest<PublicUser>
    {
        public Guid Uuid;
    }
}
