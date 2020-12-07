using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class DeleteFieldCommand : IRequest
    {
        public Guid CategoryId { get; set; }
        public Guid FieldId { get; set; }

        public DeleteFieldCommand(Guid categoryId, Guid fieldId)
        {
            CategoryId = categoryId;
            FieldId = fieldId;
        }
    }
}
