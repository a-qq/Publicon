using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }

        public DeleteCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
