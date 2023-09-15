using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class CreateJobHandler : IRequestHandler<CreateJobRequest, Unit>
    {
        private readonly IJobRepository _jobRepository;

        public CreateJobHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<Unit> Handle(CreateJobRequest request, CancellationToken token)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                PaymentType = request.PaymentType,
                Price = request.Price,
                CandidateLevel = request.CandidateLevel,
                PublicationDate = DateTime.Now,
                AuthorUuid = request.UserUuid,
                Proposals = 0,
            };

            await _jobRepository.Add(job, token);
            await _jobRepository.Save(token);

            return Unit.Value;
        }
    }
}
