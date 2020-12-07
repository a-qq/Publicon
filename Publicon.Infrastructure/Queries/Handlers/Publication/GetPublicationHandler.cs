using AutoMapper;
using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class GetPublicationHandler : IRequestHandler<GetPublicationQuery, PublicationDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IPublicationRepository _publicationRepository;

        public GetPublicationHandler(
            IMapper mapper,
            IPublicationRepository publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
        }

        public async Task<PublicationDetailsDTO> Handle(GetPublicationQuery request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if (publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            var publicationDTO = _mapper.Map<PublicationDetailsDTO>(publication);
            return publicationDTO;
        }
    }
}
