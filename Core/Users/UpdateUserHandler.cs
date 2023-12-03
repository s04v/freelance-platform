using Core.Common;
using Core.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserRequest request, CancellationToken token)
        {
            var user = await _repository.GetUser(o => o.Uuid == request.UserUuid, token)
                ?? throw new ErrorsException("User not found");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Description = request.Description;

            await _repository.Save(token);
        }
    }
}
