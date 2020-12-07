using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Queries.Models.Publication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Publication
{
    public class GetExcelDetailedRaportHandler : IRequestHandler<GetExcelDetailedRaportQuery, MemoryStream>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublicationRepository _publicationRepository;

        public GetExcelDetailedRaportHandler(
            ICategoryRepository categoryRepository,
            IPublicationRepository publicationRepository)
        {
            _categoryRepository = categoryRepository;
            _publicationRepository = publicationRepository;
        }

        public async Task<MemoryStream> Handle(GetExcelDetailedRaportQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            var list = new List<Guid>
            {
                request.CategoryId
            };

            var publications = await _publicationRepository.FilterAndSearchAsync(
                list, request.UserIds, request.SearchQuery, 0, 0);
            string c = ";";
            var fields = category.Fields.OrderBy(f => f.Id);
            var lines = new List<string>
            {
                "Title;Description;PublicationTime;AddedAt;AddedBy;Category;" + string.Join(';', fields.Select(f => f.Name).ToList()) + c
            };

            foreach (var publication in publications.Item2)
            {
                var line = publication.Title + c
                + publication.Description + c
                + publication.PublicationTime.ToShortDateString() + c
                + publication.AddedAt.ToString() + c
                + (publication?.User?.GivenName ?? "") + " " + (publication?.User?.FamilyName ?? "") + c
                + publication.Category.Name + c;

                foreach (var field in fields)
                    line += (publication.PublicationFields.FirstOrDefault(f => f.FieldId == field.Id)?.Value ?? "");

                lines.Add(line);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(string.Join(Environment.NewLine,lines)));
        }
    }
}
