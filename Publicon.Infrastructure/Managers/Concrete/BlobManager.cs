using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using System.IO;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Managers.Concrete
{
    public class BlobManager : IBlobManager
    {
        private readonly BlobContainerClient _blobContainerClient;
        public BlobManager(BlobServiceClient blobServiceClient)
        {
            _blobContainerClient = blobServiceClient.GetBlobContainerClient("publications");
        }

        public async Task<BlobInfoDTO> GetBlobAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            var blobDownloadInfo = await blobClient.DownloadAsync();
            return new BlobInfoDTO(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task UploadFileBlobAsync(IFormFile file, string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType, ContentDisposition = file.ContentDisposition });
        }

        public async Task DeleteBlobAsync(string fileName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
