using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class SoftDeleteCategoryHandler : IRequestHandler<SoftDeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public SoftDeleteCategoryHandler(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(SoftDeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            if (category.Publications.Any())
                throw new PubliconException(ErrorCode.EnitityWithExistingValues(typeof(Core.Entities.Concrete.Category)), "Cannot soft delete with existing publications in this category!");

            _categoryRepository.Delete(category);

            await _categoryRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
