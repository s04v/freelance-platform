using AutoMapper;
using Core.Common;
using Core.Jobs.Applications.Entities;
using Core.Jobs.Applications.Requests;
using Core.Jobs.Applications.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class GetMyApplicationHandler : IRequestHandler<GetMyApplicationRequest, MyApplicationResponse>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetMyApplicationHandler(IJobRepository repository)
        {
            _repository = repository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobApplication, MyApplicationResponse>();
            });

            _mapper = new Mapper(config);
        }

        public async Task<MyApplicationResponse> Handle(GetMyApplicationRequest request, CancellationToken token)
        {
            var application = await _repository.GetMyApplicationForJob(request.JobUuid, request.UserUuid, token)
                ?? throw new ErrorsException("Application not found");
            
            var applicationResponse = _mapper.Map<MyApplicationResponse>(application);

            return applicationResponse;
        }
    }
}
