using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class UpsertPublicationFileCommand : IRequest
    {
        [JsonIgnore]
        public Guid PublicationId { get; set; }
        public IFormFile File { get; set; }

        public UpsertPublicationFileCommand SetPublicationId(Guid publicationId)
        {
            PublicationId = publicationId;
            return this;
        }
    }
}
