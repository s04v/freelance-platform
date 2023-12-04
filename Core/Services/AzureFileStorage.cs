using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AzureFileStorage : IFileStorage
    {
        private readonly BlobServiceClient _blobService;
        private BlobContainerClient _attachmentsContainer;
        private BlobContainerClient _photoContainer;

        public AzureFileStorage(BlobServiceClient blobService)
        {
            _blobService = blobService;
            _attachmentsContainer = _blobService.GetBlobContainerClient("attachments");
            _photoContainer = _blobService.GetBlobContainerClient("photo");
        }

        public async Task<Stream> GetFile(string fileName)
        {
            var file = _attachmentsContainer.GetBlobClient(fileName);

            var blobDownloadInfo = await file.DownloadAsync();

            var memoryStream = new MemoryStream();
            await blobDownloadInfo.Value.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task SaveFile(IFormFile file, string fileName)
        {
            var blobClient = _attachmentsContainer.GetBlobClient(fileName);

            await using(Stream data = file.OpenReadStream())
            {
                await blobClient.UploadAsync(data);
            }
        }

        public async Task SavePhoto(IFormFile file, string fileName)
        {
            var blobClient = _photoContainer.GetBlobClient(fileName);

            await using (Stream data = file.OpenReadStream())
            {
                await blobClient.UploadAsync(data);
            }
        }
    }
}
