using MediatR;
using Publicon.Infrastructure.DTOs;
using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class CreateFieldCommand : IRequest<FieldDTO>
    {
        [JsonIgnore]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }

        public CreateFieldCommand SetCategoryId(Guid categoryId)
        {
            CategoryId = categoryId;
            return this;
        }
    }
}
