using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _rootPath;

        public LocalFileStorage(IConfiguration config)
        {
            _rootPath = config.GetValue<string>("FileStorage:LocalRootPath");

        }
        public async Task<Stream?> GetFile(string fileName)
        {
            var path = Path.Combine(_rootPath, fileName);
            FileStream stream = null;
            
            if (File.Exists(path))
            {
                stream = File.OpenRead(path);
            }
            
            return await Task.FromResult(stream);
        }

        public async Task SaveFile(IFormFile file, string fileName)
        {
            var path = Path.Combine(_rootPath, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }

        }

        public async Task SavePhoto(IFormFile file, string fileName)
        {
            var path = Path.Combine(_rootPath, "photo", fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var fileStream = new FileStream(path, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
