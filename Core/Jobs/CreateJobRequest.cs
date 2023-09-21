using Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class CreateJobRequest : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public JobType Type { get; set; }
        public JobPaymentType PaymentType { get; set; }
        public decimal Price { get; set;  }
        public CandidateLevel CandidateLevel { get; set; }
        public IFormFile[] Attachments { get; set; }
        public Guid UserUuid { get; set; }
    }
}
