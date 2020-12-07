using MediatR;
using Publicon.Infrastructure.DTOs;
using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Queries.Models.Category
{
    public class GetCategoryQuery : IRequest<CategoryDTO>
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public GetCategoryQuery(Guid id)
        {
            Id = id;
        }
    }
}
