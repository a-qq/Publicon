using MediatR;
using System;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class ValidateOwnershipQuery : AuthQuery, IRequest<bool>
    {
        public Guid PublicationId { get; set; }
        public ValidateOwnershipQuery(Guid publicationId, Guid userId)
        {
            PublicationId = publicationId;
            UserId = userId;
        }
    }
}
