using AutoMapper;
using Core.Common;
using Core.Jobs.Applications.Entities;
using Core.Jobs.Applications.Responses;
using Core.Jobs.Entities;
using Core.Jobs.Requests;
using Core.Users.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetJobHandler : IRequestHandler<GetJobRequest, JobResponseItem>
    {
        private readonly IJobRepository _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetJobHandler(IJobRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Job, JobResponseItem>();

                cfg.CreateMap<JobApplication, JobApplicationResponse>();

                cfg.CreateMap<User, ApplicationAuthor>()
                    .ForMember(o => o.Uuid, opt => opt.MapFrom(o => o.Uuid))
                    .ForMember(o => o.FullName, opt => opt.MapFrom(o => $"{o.FirstName} {o.LastName}"));
            });

            _mapper = new Mapper(config);
        }

        public async Task<JobResponseItem> Handle(GetJobRequest request, CancellationToken token)
        {
            var job = await _repository.GetJob(o => o.Uuid == request.JobUuid, token)
                ?? throw new ErrorsException("Job not found");
            
            var jobResponse = _mapper.Map<JobResponseItem>(job);

            jobResponse.ProposalsRange = Helpers.GetJobProposalsRange(job.Proposals.Value);

            var clientInfo = await _mediator.Send(new GetClientInfoRequest { Uuid = job.AuthorUuid }, token);

            jobResponse.ClientInfo = clientInfo;

            if (request.UserUuid.HasValue && job.AuthorUuid == request.UserUuid.Value)
            {
                var applications = await _repository.GetApplications(job.Uuid, token);

                jobResponse.Applications = _mapper.Map<List<JobApplicationResponse>>(applications);
            }

            var attachments = await _repository.GetJobAttachmentByJobUuid(job.Uuid, token);

            jobResponse.Attachments = attachments;

            return jobResponse;
        }
    }
}
