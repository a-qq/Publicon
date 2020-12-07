using System;
using System.Collections.Generic;

namespace Publicon.Infrastructure.DTOs
{
    public class PublicationDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationTime { get; set; }
        public string Description { get; set; }
        public DateTime AddedAt { get; set; }
        public string UserGivenName { get; set; }
        public string UserFamilyName { get; set; }
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
        public bool HasUploadedFile { get; set; }
        public IEnumerable<PublicationFieldDTO> PublicationFields { get; set; }
    }
}
