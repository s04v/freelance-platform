using Core.Jobs.Applications.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class ApplyForJobRequest : IRequest<JobApplication>
    {
        public Guid UserUuid { get; set; }
        public Guid JobUuid { get; set; }
        public string? Message { get; set; }
    }
}
