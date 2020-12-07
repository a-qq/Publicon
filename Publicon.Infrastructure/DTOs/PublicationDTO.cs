using System;

namespace Publicon.Infrastructure.DTOs
{
    public class PublicationDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationTime { get; set; }
        public DateTime AddedAt { get; set; }
        public Guid UserId { get; set; }
        public string UserGivenName { get; set; }
        public string UserFamilyName { get; set; }
        public string CategoryName { get; set; }
    }
}
