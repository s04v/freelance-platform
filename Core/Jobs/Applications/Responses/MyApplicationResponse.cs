using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications.Responses
{
    public class MyApplicationResponse
    {
        public Guid Uuid { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Message { get; set; }
    }
}
