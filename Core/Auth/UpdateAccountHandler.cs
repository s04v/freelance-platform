using Core.Common;
using Core.Services;
using Core.Users;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IMailService _mailService;

        public UpdateAccountHandler(IUserRepository userRepository, IMailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        public async Task Handle(UpdateAccountRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(o => o.Uuid == request.UserUuid, token)
                ?? throw new ErrorsException("User not found");

            if (!request.OldPassword.IsNullOrEmpty() && !request.OldPassword.IsNullOrEmpty())
            {
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
            }
        
            if (request.Email != user.Email)
            {
                var existingUserWithNewEmail = await _userRepository.GetUserByEmail(request.Email, token);

                if (existingUserWithNewEmail == null)
                {
                    string activateToken = Guid.NewGuid().ToString();

                    user.ActivateToken = activateToken;
                    user.NewEmail = request.Email;

                    await _mailService.SendEmailChangeConfirm(request.Email, activateToken);
                }
            }

            await _userRepository.Save(token);
        }
    }
}
