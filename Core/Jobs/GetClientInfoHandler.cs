using Core.Common;
using Core.Jobs.Entities;
using Core.Jobs.Requests;
using Core.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetClientInfoHandler : IRequestHandler<GetClientInfoRequest, ClientInfo>
    {
        private readonly IJobRepository _repository;
        private readonly IUserRepository _userRepository;

        public GetClientInfoHandler(IJobRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<ClientInfo> Handle(GetClientInfoRequest request, CancellationToken token)
        {
            var user = await _userRepository.GetUser(u => u.Uuid == request.Uuid, token) 
                ?? throw new ErrorsException("User not found");

            var jobs = await _repository.GetJobsOfAuthor(request.Uuid, token);

            var clientInfo = new ClientInfo
            {
                Uuid = user.Uuid,
                FullName = $"{user.FirstName} {user.LastName}",
                PostedJobs = 0,
                SpentMoney = 0
            };

            foreach (var job in jobs) 
            {
                clientInfo.PostedJobs++;
                clientInfo.SpentMoney += job.Price;
            }
            
            return clientInfo;
        }
    }
}
