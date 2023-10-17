using Core.Common;
using Core.Users;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class EmailChangeConfirmHandler : IRequestHandler<EmailChangeConfirmRequest>
    {
        private readonly IUserRepository _repository;

        public EmailChangeConfirmHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EmailChangeConfirmRequest request, CancellationToken token)
        {
            var user = await _repository.GetUserByActivateToken(request.ActivateToken, token)
                ?? throw new ErrorsException("Not found");

            user.Email = user.NewEmail;
            user.NewEmail = null;
            user.ActivateToken = null;

            await _repository.Save(token);
        }
    }
}
