using Microsoft.AspNetCore.Http;
using Publicon.Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Managers.Abstract
{
    public interface IBlobManager
    {
        Task<BlobInfoDTO> GetBlobAsync(string fileName);
        Task UploadFileBlobAsync(IFormFile file, string fileName);
        Task DeleteBlobAsync(string fileName);
    }
}
