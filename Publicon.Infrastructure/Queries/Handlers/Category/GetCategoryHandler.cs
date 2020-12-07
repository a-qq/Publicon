using AutoMapper;
using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Queries.Models.Category;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Queries.Handlers.Category
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }
    }
}
