using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Publication
{
    public class DeletePublicationFileHandler : IRequestHandler<DeletePublicationFileCommand, Unit>
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IBlobManager _blobManager;

        public DeletePublicationFileHandler(
            IPublicationRepository publicationRepository,
            IBlobManager blobManager)
        {
            _publicationRepository = publicationRepository;
            _blobManager = blobManager;
        }

        public async Task<Unit> Handle(DeletePublicationFileCommand request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if (publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            if (string.IsNullOrWhiteSpace(publication.FileName))
                throw new PubliconException(ErrorCode.FileNotFound);

            await _blobManager.DeleteBlobAsync(publication.FileName);
            publication.SetFileName(null);

            await _publicationRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
