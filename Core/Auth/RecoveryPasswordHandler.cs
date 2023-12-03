using Core.Auth.Requests;
using Core.Common;
using Core.Services;
using Core.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class RecoveryPasswordHandler : IRequestHandler<RecoveryPasswordRequest, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;

        public RecoveryPasswordHandler(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public async Task<Unit> Handle(RecoveryPasswordRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(o => o.Email == request.Email, token);

            if (user == null)
            {
                throw new ErrorsException("User not found");
            }

            var recoveryToken = Guid.NewGuid().ToString("N");

            user.RecoveryToken = recoveryToken;

            await _userRepository.Save(token);

            await _mailService.SendRecoveryPassword(request.Email, recoveryToken, token);

            return Unit.Value;
        }
    }
}
