using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class SoftDeleteCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }

        public SoftDeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
