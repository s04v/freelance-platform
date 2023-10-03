using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IFileStorage
    {
        Task SaveFile(IFormFile file, string fileName);
        Task SavePhoto(IFormFile file, string fileName);
        Task<FileStream> GetFile(string fileName);
    }
}
