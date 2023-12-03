using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications.Entities
{
    public class ApplicationAuthor
    {
        public Guid Uuid { get; set; }
        public string? FullName { get; set; }
    }
}
