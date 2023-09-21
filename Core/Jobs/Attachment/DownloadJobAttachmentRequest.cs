using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Attachment
{
    public class DownloadJobAttachmentRequest : IRequest<DownloadJobAttachmentResponse>
    {
        public Guid AttachmentUuid { get; set; }
    }
}
