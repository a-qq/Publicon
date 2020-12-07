using MediatR;
using Publicon.Infrastructure.DTOs;
using System;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class DownloadPublicationQuery : IRequest<BlobInfoDTO>
    {
        public Guid PublicationId { get; set; }

        public DownloadPublicationQuery(Guid publicationId)
        {
            PublicationId = publicationId;
        }
    }
}