using System;

namespace Publicon.Infrastructure.DTOs
{
    public class FieldDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
    }
}
