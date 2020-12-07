using MediatR;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class DeleteFieldHandler : IRequestHandler<DeleteFieldCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteFieldHandler(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            var field = category.Fields.FirstOrDefault(f => f.Id == request.FieldId);
            if(field == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Field)));

            if (field.IsRequired && field.PublicationFields.Any())
                throw new PubliconException(ErrorCode.EnitityWithExistingValues(typeof(Field)), $"Change {typeof(Field).Name} to unrequired or delete all connected publications in order to delete!");

            category.DeleteField(field);
            await _categoryRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
