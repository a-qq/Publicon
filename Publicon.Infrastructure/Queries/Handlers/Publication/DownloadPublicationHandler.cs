using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class DownloadPublicationHandler : IRequestHandler<DownloadPublicationQuery, BlobInfoDTO>
    {
        private readonly IBlobManager _blobManager;
        private readonly IPublicationRepository _publicationRepository;

        public DownloadPublicationHandler(
            IBlobManager blobManager,
            IPublicationRepository publicationRepository)
        {
            _blobManager = blobManager;
            _publicationRepository = publicationRepository;
        }

        public async Task<BlobInfoDTO> Handle(DownloadPublicationQuery request, CancellationToken cancellationToken)
        {

            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if (publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            if (string.IsNullOrWhiteSpace(publication.FileName))
                throw new PubliconException(ErrorCode.FileNotFound);
            string regexSearch = new string(Path.GetInvalidFileNameChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var blobDTO = await _blobManager.GetBlobAsync(publication.FileName);
            blobDTO.FileName = r.Replace(publication.Title, "");
            return blobDTO;
        }
    }
}
