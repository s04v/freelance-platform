using AutoMapper;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class GetMyApplicationsHandler : IRequestHandler<GetMyApplicationsRequest, IEnumerable<MyApplicationWithJob>>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetMyApplicationsHandler(IJobRepository repository)
        {
            _repository = repository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobApplication, MyApplicationWithJob>()
                    .ForMember(o => o.JobUuid, opt => opt.MapFrom(o => o.Job.Uuid))
                    .ForMember(o => o.Title, opt => opt.MapFrom(o => o.Job.Title))
                    .ForMember(o => o.Description, opt => opt.MapFrom(o => o.Job.Description))
                    .ForMember(o => o.Type, opt => opt.MapFrom(o => o.Job.Type))
                    .ForMember(o => o.PaymentType, opt => opt.MapFrom(o => o.Job.PaymentType))
                    .ForMember(o => o.Price, opt => opt.MapFrom(o => o.Job.Price))
                    .ForMember(o => o.CandidateLevel, opt => opt.MapFrom(o => o.Job.CandidateLevel))
                    .ForMember(o => o.PublicationDate, opt => opt.MapFrom(o => o.Job.PublicationDate))
                    .ForMember(o => o.ProposalsRange, opt => opt.MapFrom(o => Helpers.GetJobProposalsRange(o.Job.Proposals.Value)))
                    .ForMember(o => o.ApplicationUuid, opt => opt.MapFrom(o => o.Uuid))
                    .ForMember(o => o.ApplicationDate, opt => opt.MapFrom(o => o.ApplicationDate))
                    .ForMember(o => o.Message, opt => opt.MapFrom(o => o.Message));
            });

            _mapper = new Mapper(config);
        }

        public async Task<IEnumerable<MyApplicationWithJob>> Handle(GetMyApplicationsRequest request, CancellationToken token)
        {
            var application = await _repository.GetMyApplicationsWithJobs(request.UserUuid, token);

            var response = _mapper.Map<IEnumerable<MyApplicationWithJob>>(application);

            return response;
        }
    }
}
