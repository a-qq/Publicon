using AutoMapper;
using MediatR;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Category
{
    public class CreateFieldHandler : IRequestHandler<CreateFieldCommand, FieldDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateFieldHandler(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<FieldDTO> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            if (category.Fields.Any(f => f.Name == request.Name))
                throw new PubliconException(ErrorCode.NameNotUnique);

            var field = _mapper.Map<Field>(request);
            category.AddField(field);

            await _categoryRepository.SaveChangesAsync();

            if(request.IsRequired && category.Publications.Any())
            {
                foreach(var publication in category.Publications)
                {
                    publication.AddPublicationField(new PublicationField(field.Id, request.DefaultValue));
                }

                await _categoryRepository.SaveChangesAsync();
            }

            var fieldDTO = _mapper.Map<FieldDTO>(field);
            return fieldDTO;
        }
    }
}
