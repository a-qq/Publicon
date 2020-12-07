using MediatR;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Queries.Models.Publication;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class GetExcelSimpleRaportHandler : IRequestHandler<GetExcelSimpleRaportQuery, MemoryStream>
    {
        private readonly IPublicationRepository _publicationRepository;

        public GetExcelSimpleRaportHandler(
            IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<MemoryStream> Handle(GetExcelSimpleRaportQuery request, CancellationToken cancellationToken)
        {
            var publications = await _publicationRepository.FilterAndSearchAsync(
                request.CategoryIds, request.UserIds, request.SearchQuery, 0, 0);
            string c = ";";
            var csvstring = "Title;Description;PublicationTime;AddedAt;AddedBy;Category;" + Environment.NewLine;

            foreach (var publication in publications.Item2)
            {
                csvstring = csvstring
                    + publication.Title + c
                    + publication.Description + c
                    + publication.PublicationTime.ToShortDateString() + c
                    + publication.AddedAt.ToString() + c
                    + (publication?.User?.GivenName ?? "") + " " + (publication?.User?.FamilyName ?? "") + c
                    + publication.Category.Name + c + Environment.NewLine;
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(csvstring));
        }
    }
}
