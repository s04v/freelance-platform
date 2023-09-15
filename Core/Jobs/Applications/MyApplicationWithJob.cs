using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class MyApplicationWithJob
    {
        public Guid JobUuid { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public JobType Type { get; set; }
        public JobPaymentType PaymentType { get; set; }
        public decimal Price { get; set; }
        public CandidateLevel CandidateLevel { get; set; }
        public DateTime PublicationDate { get; set; }
        public ProposalsRange ProposalsRange { get; set; }

        public Guid ApplicationUuid { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Message { get; set; }
    }
}
