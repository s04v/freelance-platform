using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class RevokeApplicationHandler : IRequestHandler<RevokeApplicationRequest>
    {
        private readonly IJobRepository _repository;

        public RevokeApplicationHandler(IJobRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RevokeApplicationRequest request, CancellationToken token)
        {
            /*var application = await _repository.GetApplication(
                o => o.Uuid == request.ApplicationUuid && o.UserUuid == request.UserUuid,
                token)
                ?? throw new ErrorsException("Applciation not found");
            */
            var application = new JobApplication
            {
                UserUuid = request.UserUuid,
                Uuid = request.ApplicationUuid,
            };

            _repository.Remove<JobApplication>(application);
            await _repository.Save(token);
        }
    }
}
