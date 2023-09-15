using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class JobListResponse
    {
        public int PageCount { get; set; }
        public IEnumerable<JobResponseItem> Jobs { get; set; }
    }
}
