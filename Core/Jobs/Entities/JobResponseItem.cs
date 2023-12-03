using Core.Common;
using Core.Jobs.Applications.Responses;
using Core.Jobs.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Entities
{
    public class JobResponseItem
    {
        public Guid Uuid { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public JobType Type { get; set; }
        public decimal Price { get; set; }
        public CandidateLevel CandidateLevel { get; set; }
        public DateTime PublicationDate { get; set; }
        public ProposalsRange ProposalsRange { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public IEnumerable<JobApplicationResponse>? Applications { get; set; }
        public IEnumerable<JobAttachment>? Attachments { get; set; }
    }
}
