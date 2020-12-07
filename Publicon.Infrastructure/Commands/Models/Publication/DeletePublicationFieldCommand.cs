using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class DeletePublicationFieldCommand : IRequest
    {
        public Guid PublicationId { get; set; }
        public Guid FieldId { get; set; }

        public DeletePublicationFieldCommand(Guid publicationId, Guid fieldId)
        {
            PublicationId = publicationId;
            FieldId = fieldId;
        }
    }
}
