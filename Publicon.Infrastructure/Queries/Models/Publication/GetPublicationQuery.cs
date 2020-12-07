using MediatR;
using Publicon.Infrastructure.DTOs;
using System;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class GetPublicationQuery : IRequest<PublicationDetailsDTO>
    {
        public Guid PublicationId { get; set; }

        public GetPublicationQuery(Guid publicationId)
        {
            PublicationId = publicationId;
        }
    }
}
