using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Publication
{
    public class UpsertPublicationFileHandler : IRequestHandler<UpsertPublicationFileCommand, Unit>
    {
        private readonly IBlobManager _blobManager;
        private readonly IPublicationRepository _publicationRepository;

        public UpsertPublicationFileHandler(
            IBlobManager blobManager,
            IPublicationRepository publicationRepository)
        {
            _blobManager = blobManager;
            _publicationRepository = publicationRepository;
        }

        public async Task<Unit> Handle(UpsertPublicationFileCommand request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if(publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            if (string.IsNullOrWhiteSpace(publication.FileName))
            {
                publication.SetFileName(Guid.NewGuid().ToString());
                await _publicationRepository.SaveChangesAsync();
            }

            await _blobManager.UploadFileBlobAsync(request.File, publication.FileName);
            return Unit.Value;
        }
    }
}
