using MediatR;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Publication;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Publication
{
    public class DeletePublicationFieldHandler : IRequestHandler<DeletePublicationFieldCommand, Unit>
    {
        private readonly IPublicationRepository _publicationRepository;

        public DeletePublicationFieldHandler(
            IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<Unit> Handle(DeletePublicationFieldCommand request, CancellationToken cancellationToken)
        {
            var publication = await _publicationRepository.GetByIdAsync(request.PublicationId);
            if (publication == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Publication)));

            var field = publication.Category.Fields.FirstOrDefault(f => f.Id == request.FieldId);
            if (field == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Field)));

            if (field.IsRequired)
                throw new PubliconException(ErrorCode.FieldRequired, "Cannot delete a required field!");

            var publicationField = publication.PublicationFields.FirstOrDefault(pf => pf.FieldId == request.FieldId);
            if (publicationField == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(PublicationField)));

            publication.DeletePublicationField(publicationField);
            await _publicationRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
