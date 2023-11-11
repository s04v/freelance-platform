using AutoMapper;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, PublicUser>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserHandler(IUserRepository repository)
        {
            _repository = repository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, PublicUser>();
            });

            _mapper = new Mapper(config);
        }
        public async Task<PublicUser> Handle(GetUserRequest request, CancellationToken token)
        {
            var user = await _repository.GetUser(o => o.Uuid == request.Uuid, token)
                ?? throw new ErrorsException("User not found");

            var publicUser = _mapper.Map<PublicUser>(user);

            return publicUser;
        }
    }
}
