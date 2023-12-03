using Core.Auth.Requests;
using Core.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        private readonly IUserRepository _repository;

        public RegisterUserValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.Email).NotEmpty().EmailAddress().CustomAsync(CheckIfUserExists);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
        }

        public async Task<bool> CheckIfUserExists(string email, ValidationContext<RegisterUserRequest> context, CancellationToken token)
        {
            var userExists = await _repository.CheckIfUserWithEmailExists(email, token);
            if (userExists)
            {
                context.AddFailure("User with given email exists");
            }

            return userExists;
        }
    }
}
