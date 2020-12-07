using System;

namespace Publicon.Infrastructure.DTOs
{
    public class PublicationFieldDTO
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
