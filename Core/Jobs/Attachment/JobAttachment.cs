using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Attachment
{
    public class JobAttachment
    {
        public Guid Uuid { get; set; }
        public string FileName { get; set; }
        public Guid JobUuid { get; set; }
        public string ContentType { get; set; }
        public Job Job { get; set; }
    }
}
