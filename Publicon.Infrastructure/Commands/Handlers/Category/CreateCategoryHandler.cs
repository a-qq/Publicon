using AutoMapper;
using MediatR;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.ExistByNameAsync(request.Name))
                throw new PubliconException(ErrorCode.NameNotUnique);

            var category = _mapper.Map<Core.Entities.Concrete.Category>(request);
            var fields = _mapper.Map<IEnumerable<Field>>(request.Fields);

            category.AddFields(fields);
            _categoryRepository.Add(category);

            await _categoryRepository.SaveChangesAsync();

            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }
    }
}
