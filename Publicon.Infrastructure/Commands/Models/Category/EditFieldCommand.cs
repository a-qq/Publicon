using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class EditFieldCommand : IRequest
    {
        [JsonIgnore]
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Guid FieldId { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }
        public string DefaultValue { get; set; }

        public EditFieldCommand SetIds(Guid categoryId, Guid fieldId)
        {
            CategoryId = categoryId;
            FieldId = fieldId;
            return this;
        }
    }
}
