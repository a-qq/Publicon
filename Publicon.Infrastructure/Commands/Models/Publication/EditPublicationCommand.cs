using MediatR;
using Publicon.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class EditPublicationCommand : Command, IRequest 
    {
        [JsonIgnore]
        public Guid PublicationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationTime { get; set; }
        public IEnumerable<PublicationFieldToManipulateDTO> PublicationFields { get; set; }

        public EditPublicationCommand SetPublicationId(Guid publicationId)
        {
            PublicationId = publicationId;
            return this;
        }
    }
}
