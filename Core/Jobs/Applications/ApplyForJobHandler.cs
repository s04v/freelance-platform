using Core.Common;
using Core.Jobs.Applications.Entities;
using Core.Jobs.Applications.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class ApplyForJobHandler : IRequestHandler<ApplyForJobRequest, JobApplication>
    {
        private readonly IJobRepository _repository;

        public ApplyForJobHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<JobApplication> Handle(ApplyForJobRequest request, CancellationToken token)
        {
            var job = await _repository.GetJob(o => o.Uuid == request.JobUuid, token);

            if (job == null) 
            {
                throw new ErrorsException("Job not found");
            }

            var application = new JobApplication
            {
                Uuid = Guid.NewGuid(),
                UserUuid = request.UserUuid,
                JobUuid = request.JobUuid,
                Message = request.Message,
                ApplicationDate = DateTime.Now,
                Status = ApplicationStatus.New,
            };

            job.Proposals++;
            await _repository.CreateApplication(application, token);

            await _repository.Save(token);

            return application;
        }
    }
}
