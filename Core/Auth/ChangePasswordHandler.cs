using Core.Common;
using Core.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Core.Auth
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Unit>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ChangePasswordRequest request, CancellationToken token)
        {
            User? user = null; 

            if (request.UserUuid.HasValue)
            {
                user = await _userRepository.GetUser(o => o.Uuid == request.UserUuid, token);
            }
            else if (!string.IsNullOrEmpty(request.RecoveryToken)) 
            {
                user = await _userRepository.GetUser(o => o.RecoveryToken == request.RecoveryToken, token);
            }

            if (user == null)
            {
                throw new ErrorsException("User not found");
            }

            string passwordHash = BCryptNet.HashPassword(request.NewPassword, 10);

            user.Password = passwordHash;
            user.RecoveryToken = null;
            await _userRepository.Save(token);

            return Unit.Value;
        }
    }
}
