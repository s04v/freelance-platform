using Core.Users.Entities;
using Core.Users.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class GetMeHandler : IRequestHandler<GetMeRequest, User>
    {
        private readonly IUserRepository _repository;

        public GetMeHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetMeRequest request, CancellationToken token)
        {
            var user = await _repository.GetUser(o => o.Uuid == request.UserUuid, token);
            
            return user;
        }
    }
}
