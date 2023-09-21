using Core.Common;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Attachment
{
    public class DownloadJobAttachmentHandler : IRequestHandler<DownloadJobAttachmentRequest, DownloadJobAttachmentResponse>
    {
        private readonly IFileStorage _fileStorage;
        private readonly IJobRepository _repository;

        public DownloadJobAttachmentHandler(IJobRepository repository, IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
            _repository = repository;
        }

        public async Task<DownloadJobAttachmentResponse> Handle(DownloadJobAttachmentRequest request, CancellationToken token)
        {
            var fileContent = await _fileStorage.GetFile(request.AttachmentUuid.ToString("N"));

            var jobAttachment = await _repository.GetJobAttachmentByUuid(request.AttachmentUuid, token)
                ?? throw new ErrorsException("Attachment not found");

            var response = new DownloadJobAttachmentResponse 
            { 
                FileContent = fileContent,
                ContentType = jobAttachment.ContentType,
                FileName = jobAttachment.FileName
            };

            return response;
        }
    }
}
