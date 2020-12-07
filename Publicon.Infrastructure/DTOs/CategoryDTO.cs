using System;
using System.Collections.Generic;

namespace Publicon.Infrastructure.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<FieldDTO> Fields { get; set; }
    }
}
