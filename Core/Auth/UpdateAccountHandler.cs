using Core.Common;
using Core.Users;
using FluentValidation;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Core.Auth
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountRequest>
    {
        private readonly IUserRepository _userRepository;

        public UpdateAccountHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateAccountRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(o => o.Uuid == request.UserUuid, token);

            if (request.OldPassword != null)
            {
                if (user == null)
                {
                    throw new ErrorsException("User not found");
                }

                bool passwordVerify = BCryptNet.Verify(request.OldPassword, user.Password);

                if (!passwordVerify)
                {
                    throw new ErrorsException("Passwords do not match");
                }

                var validator = new InlineValidator<UpdateAccountRequest>();
                validator.RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6).MaximumLength(50);

                var validatioResult = await validator.ValidateAsync(request);

                if (!validatioResult.IsValid)
                {
                    throw new ErrorsException("New password validation failed");
                }

                string passwordHash = BCryptNet.HashPassword(request.NewPassword, 10);

                user.Password = passwordHash;
                user.RecoveryToken = null;

                await _userRepository.Save(token);
            }
        }
    }
}
