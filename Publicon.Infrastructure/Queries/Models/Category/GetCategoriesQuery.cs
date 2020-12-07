using MediatR;
using Publicon.Infrastructure.DTOs;
using System.Collections.Generic;

namespace Publicon.Infrastructure.Queries.Models.Category
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDTO>>
    {
        public bool? IsArchived { get; set; }
    }
}
