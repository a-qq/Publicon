using Publicon.Infrastructure.Helpers;
using System.Collections.Generic;

namespace Publicon.Infrastructure.DTOs
{
    public class PublicationPaginatedListDTO
    {
        public IEnumerable<PublicationDTO> Publications { get; set; }
        public PaginationMetadata Metadata { get; set; }
    }
}
