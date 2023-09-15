using Core.Common;
using Core.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Core.Auth
{
    public class LoginWithCredentialsHandler : IRequestHandler<LoginWithCredentialsRequest, LoginResponse>
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public LoginWithCredentialsHandler(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<LoginResponse> Handle(LoginWithCredentialsRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(o => o.Email == request.Email, token);

            if (user == null)
            {
                throw new ErrorsException("Email or password is wrong");
            }

            bool passwordVerify = BCryptNet.Verify(request.Password, user.Password);
            
            if (!passwordVerify)
            {
                throw new ErrorsException("Email or password is wrong");
            }

            var jwtToken = GenerateJWT(user);
            var response = new LoginResponse 
            { 
                Token = jwtToken
            };

            return response;
        }

        public string GenerateJWT(User user)
        {
            var key = _config.GetValue<string>("Jwt:Key");
            var issuer = _config.GetValue<string>("Jwt:Issuer");
            var audience = _config.GetValue<string>("Jwt:Audience");

            if (string.IsNullOrEmpty(key))
            {
                throw new ErrorsException("Internal error");
            }

            var claims = new List<Claim>
            {
                new Claim("id", user.Uuid.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
