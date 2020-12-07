using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class DeletePublicationFileCommand : IRequest
    {
        public Guid PublicationId { get; set; }

        public DeletePublicationFileCommand(Guid publicationId)
        {
            PublicationId = publicationId;
        }
    }
}
