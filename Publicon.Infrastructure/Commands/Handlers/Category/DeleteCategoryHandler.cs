using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.Managers.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IBlobManager _blobManager;
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(
            IBlobManager blobManager,
            ICategoryRepository categoryRepository)
        {
            _blobManager = blobManager;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            foreach (var publication in category.Publications)
            {
                if (!string.IsNullOrWhiteSpace(publication.FileName))
                    await _blobManager.DeleteBlobAsync(publication.FileName);
            }
            category.DeleteAllPublications();
            await _categoryRepository.SaveChangesAsync();
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
