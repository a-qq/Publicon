using AutoMapper;
using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public EditCategoryHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            if (category.Name != request.Name && await _categoryRepository.ExistByNameAsync(request.Name))
                throw new PubliconException(ErrorCode.NameNotUnique);
                
            _mapper.Map(request, category);
            await _categoryRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
