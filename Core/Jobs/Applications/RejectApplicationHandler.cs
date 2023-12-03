using Core.Common;
using Core.Jobs.Applications.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class RejectApplicationHandler : IRequestHandler<RejectApplicationRequest>
    {
        private readonly IJobRepository _repository;

        public RejectApplicationHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RejectApplicationRequest request, CancellationToken token)
        {
            var application = await _repository.GetApplication(o => o.Uuid == request.ApplicationUuid, token)
                ?? throw new ErrorsException("Application not found");

            application.Status = ApplicationStatus.Rejected;

            await _repository.Save(token);
        }
    }
}
