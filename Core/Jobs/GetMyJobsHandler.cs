using AutoMapper;
using Core.Jobs.Entities;
using Core.Jobs.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class GetMyJobsHandler : IRequestHandler<GetMyJobsRequest, IEnumerable<MyJobItem>>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetMyJobsHandler(IJobRepository repository)
        {
            _repository = repository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Job, MyJobItem>();
            });

            _mapper = new Mapper(config);
        }

        public async Task<IEnumerable<MyJobItem>> Handle(GetMyJobsRequest request, CancellationToken token)
        {
            var jobs = await _repository.GetJobsOfAuthor(request.UserUuid, token);

            var jobItems =  _mapper.Map<List<MyJobItem>>(jobs);

            return jobItems;
        }
    }
}
