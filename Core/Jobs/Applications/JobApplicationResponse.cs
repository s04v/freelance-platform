using Core.Common;
using Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications
{
    public class JobApplicationResponse
    {
        public Guid Uuid { get; set; }
        public Guid JobUuid { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Message { get; set; }
        public string? FullName { get; set; }
        public ApplicationStatus Status { get; set;  }
        public ApplicationAuthor User { get; set; }
    }
}
