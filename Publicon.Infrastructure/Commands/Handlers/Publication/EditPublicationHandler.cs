using AutoMapper;
using MediatR;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Publication
{
    public class EditPublicationHandler : IRequestHandler<EditPublicationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IPublicationRepository _publicationRepository;

        public EditPublicationHandler(
            IMapper mapper,
            IPublicationRepository publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
        }

        public async Task<Unit> Handle(EditPublicationCommand request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if (publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            if (request.PublicationFields != null && request.PublicationFields.Any())
            {
                var categoryFields = publication.Category.Fields;
                List<string> notFoundFields = new List<string>();
                foreach (var field in request.PublicationFields)
                {
                    if (!categoryFields.Any(f => f.Id == field.FieldId))
                        notFoundFields.Add(field.FieldId.ToString());
                }

                if (notFoundFields.Any())
                    throw new PubliconException(ErrorCode.EntityNotFound(typeof(Field)), "Ids: " + string.Join(", ", notFoundFields));

                foreach (var field in request.PublicationFields)
                {
                    var destField = publication.PublicationFields.FirstOrDefault(pf => pf.FieldId == field.FieldId);
                    if (destField == null)
                        publication.AddPublicationField(_mapper.Map<PublicationField>(field));
                    else
                        _mapper.Map(field, destField);
                }
            }
            _mapper.Map(request, publication);

            await _publicationRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
