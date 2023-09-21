using Core.Common;
using Core.Jobs.Applications;
using Core.Jobs.Attachment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class Job
    {
        public Guid Uuid { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public JobType Type { get; set; }
        public JobPaymentType PaymentType { get; set; }
        public decimal Price { get; set; }
        public CandidateLevel CandidateLevel { get; set; }
        public DateTime PublicationDate { get; set; }
        public Guid AuthorUuid { get; set; }
        public int? Proposals { get; set; }

        public IEnumerable<JobApplication> Applications { get; set; } = new List<JobApplication>();
        public IEnumerable<JobAttachment> Attachments { get; set; } = new List<JobAttachment>();
    }
}
