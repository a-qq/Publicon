using MediatR;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class GetPublicationsQuery : PublicationListQuery, IRequest<PublicationPaginatedListDTO> 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
