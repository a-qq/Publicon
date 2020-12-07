using MediatR;
using Publicon.Infrastructure.DTOs;
using System.Collections.Generic;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class GetDetailedPublicationsQuery 
        : PublicationListQuery, IRequest<IEnumerable<PublicationDetailsDTO>> { }
}
