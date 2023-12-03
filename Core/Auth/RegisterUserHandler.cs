using Core.Auth.Requests;
using Core.Services;
using Core.Users;
using Core.Users.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Core.Auth
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Unit>
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _repository;
        private readonly IMailService _mailService;

        public RegisterUserHandler(IConfiguration config, IUserRepository repository, IMailService mailService)
        {
            _config = config;
            _repository = repository;
            _mailService = mailService;
        }

        public RegisterUserHandler() { }

        public async Task<Unit> Handle(RegisterUserRequest request, CancellationToken token)
        {
            string activateToken = Guid.NewGuid().ToString();
            string passwordHash = BCryptNet.HashPassword(request.Password, 10);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = passwordHash,
                ActivateToken = activateToken,
            };

            await _repository.CreateUser(user, token);
            await _mailService.SendUserProfileActivate(request.Email, activateToken);

            return Unit.Value;
        }
    }
}
