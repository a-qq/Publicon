using AutoMapper;
using MediatR;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Helpers;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class GetPublicationsHandler : IRequestHandler<GetPublicationsQuery, PublicationPaginatedListDTO>
    {
        private readonly IMapper _mapper;
        private readonly IPublicationRepository _publicationRepository;

        public GetPublicationsHandler(
            IMapper mapper,
            IPublicationRepository publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
        }

        public async Task<PublicationPaginatedListDTO> Handle(GetPublicationsQuery request, CancellationToken cancellationToken)
        {
            var publications = await _publicationRepository.FilterAndSearchAsync(
                request.CategoryIds, request.UserIds, request.SearchQuery, request.PageNumber, request.PageSize);

            var publicationDTOs = _mapper.Map<IEnumerable<PublicationDTO>>(publications.Item2);
            return new PublicationPaginatedListDTO
            {
                Publications = publicationDTOs,
                Metadata = new PaginationMetadata(request.PageNumber, request.PageSize, publications.Item1)
            };
        }
            
    }
}
