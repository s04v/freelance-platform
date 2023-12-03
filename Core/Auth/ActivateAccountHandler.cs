using Core.Auth.Requests;
using Core.Common;
using Core.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class ActivateAccountHandler : IRequestHandler<ActivateAccountRequest>
    {
        private readonly IUserRepository _repository;

        public ActivateAccountHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ActivateAccountRequest request, CancellationToken token)
        {
            var user = await _repository.GetUserByActivateToken(request.ActivateToken, token);

            if (user == null)
            {
                throw new ErrorsException("Not found");
            }

            user.IsActive = true;
            user.ActivateToken = null;

            await _repository.Save(token);
        }
    }
}
