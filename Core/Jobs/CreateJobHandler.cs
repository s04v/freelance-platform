using Core.Jobs.Attachment;
using Core.Services;
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
        private readonly IFileStorage _fileStorage;

        public CreateJobHandler(IJobRepository jobRepository, IFileStorage fileStorage)
        {
            _jobRepository = jobRepository;
            _fileStorage = fileStorage;
        }

        public async Task<Unit> Handle(CreateJobRequest request, CancellationToken token)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Price = request.Price,
                CandidateLevel = request.CandidateLevel,
                PublicationDate = DateTime.Now,
                AuthorUuid = request.UserUuid,
                Proposals = 0,
            };

            await _jobRepository.Add(job, token);
            
            if (request.Attachments != null)
            {

                foreach (var attachment in request.Attachments)
                {
                    var jobAttachment = new JobAttachment
                    {
                        Uuid = Guid.NewGuid(),
                        FileName = attachment.FileName,
                        JobUuid = job.Uuid,
                        ContentType = attachment.ContentType,
                    };

                    await _jobRepository.AddJobAttachment(jobAttachment, token);

                    await _fileStorage.SaveFile(attachment, jobAttachment.Uuid.ToString("N"));
                }
            }

            await _jobRepository.Save(token);

            return Unit.Value;
        }
    }
}
