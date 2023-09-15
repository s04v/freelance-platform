using Core.Common;
using Core.Jobs.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class JobResponse
    {
        public Guid Uuid { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public JobType Type { get; set; }
        public JobPaymentType PaymentType { get; set; }
        public decimal Price { get; set; }
        public CandidateLevel CandidateLevel { get; set; }
        public DateTime PublicationDate { get; set; }
        public ProposalsRange ProposalsRange { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public IEnumerable<JobApplication>? Applications { get; set; }
    }
}
