using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class DeletePublicationCommand : IRequest
    {
        public Guid PublicationId { get; set; }

        public DeletePublicationCommand(Guid publicationId)
        {
            PublicationId = publicationId;
        }
    }
}
