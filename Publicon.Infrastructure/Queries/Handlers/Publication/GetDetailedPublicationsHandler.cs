using AutoMapper;
using MediatR;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class GetDetailedPublicationsHandler : IRequestHandler<GetDetailedPublicationsQuery, IEnumerable<PublicationDetailsDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IPublicationRepository _publicationRepository;

        public GetDetailedPublicationsHandler(
            IMapper mapper,
            IPublicationRepository publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
        }

        public async Task<IEnumerable<PublicationDetailsDTO>> Handle(GetDetailedPublicationsQuery request, CancellationToken cancellationToken)
        {
            var publications = await _publicationRepository.FilterAndSearchAsync(
                request.CategoryIds, request.UserIds, request.SearchQuery, 0, 0);

            var publicationDTOs = _mapper.Map<IEnumerable<PublicationDetailsDTO>>(publications.Item2);
            return publicationDTOs;
        }
    }
}
