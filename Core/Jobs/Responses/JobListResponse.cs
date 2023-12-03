using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Jobs.Entities;

namespace Core.Jobs.Responses
{
    public class JobListResponse
    {
        public int PageCount { get; set; }
        public IEnumerable<JobResponseItem> Jobs { get; set; }
    }
}
