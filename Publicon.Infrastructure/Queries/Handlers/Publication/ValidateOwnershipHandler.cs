using MediatR;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class ValidateOwnershipHandler : IRequestHandler<ValidateOwnershipQuery, bool>
    {
        private readonly IPublicationRepository _publicationRepository;

        public ValidateOwnershipHandler(
            IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }
        public async Task<bool> Handle(ValidateOwnershipQuery request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            return publication != null && publication.UserId == request.UserId;
        }
    }
}
