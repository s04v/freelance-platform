using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class UpdateUserPhotoRequest : IRequest
    {
        public Guid? UserUuid { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
