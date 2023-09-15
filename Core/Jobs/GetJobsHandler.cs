using AutoMapper;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetJobsHandler : IRequestHandler<GetJobsRequest, JobListResponse>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        private int pageSize = 5;

        public GetJobsHandler(IJobRepository repository)
        {
            _repository = repository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Job, JobResponseItem>()
                    .ForMember(o => o.ProposalsRange, opt => opt.MapFrom(o => Helpers.GetJobProposalsRange(o.Proposals.Value)));
            });

            _mapper = new Mapper(config);
        }

        public async Task<JobListResponse> Handle(GetJobsRequest request, CancellationToken token)
        {
            var jobs = await _repository.GetJobsPage(request.PageNumber, pageSize, token);
            var pageCount = await _repository.GetJobsCount(token) / pageSize;

            var jobsList = _mapper.Map<List<JobResponseItem>>(jobs);

            var result = new JobListResponse
            {
                Jobs = jobsList,
                PageCount = pageCount,
            };

            return result;
        }
    }
}
