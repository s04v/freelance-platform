using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Entities
{
    public class ClientInfo
    {
        public Guid Uuid { get; set; }
        public string FullName { get; set; }
        public int PostedJobs { get; set; }
        public decimal SpentMoney { get; set; }
    }
}
