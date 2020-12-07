using MediatR;
using Publicon.Infrastructure.DTOs;
using System.Collections.Generic;

namespace Publicon.Infrastructure.Commands.Models.Category
{
    public class CreateCategoryCommand : IRequest<CategoryDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<FieldToAddDTO> Fields { get; set; }
    }
}
