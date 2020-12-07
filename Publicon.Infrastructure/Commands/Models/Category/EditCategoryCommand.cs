using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class EditCategoryCommand : IRequest
    {
        [JsonIgnore]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }

        public EditCategoryCommand SetCategoryId(Guid categoryId)
        {
            CategoryId = categoryId;
            return this;
        }
    }
}
