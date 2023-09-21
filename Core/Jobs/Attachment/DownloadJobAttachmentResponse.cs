using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Attachment
{
    public class DownloadJobAttachmentResponse
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public FileStream FileContent { get; set; }
    }
}
