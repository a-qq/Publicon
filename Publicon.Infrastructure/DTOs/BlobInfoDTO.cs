using System.IO;

namespace Publicon.Infrastructure.DTOs
{
    public class BlobInfoDTO
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public BlobInfoDTO(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }

    }
}
