using AutoMapper;
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
    public class EditFieldHandler : IRequestHandler<EditFieldCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public EditFieldHandler(
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(EditFieldCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            var field = category.Fields.FirstOrDefault(f => f.Id == request.FieldId);
            if (field == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Field)));

            if (field.Name != request.Name && category.Fields.Any(f => f.Name == request.Name))
                throw new PubliconException(ErrorCode.NameNotUnique);

            if (request.IsRequired != field.IsRequired && request.IsRequired)
            {
                if (string.IsNullOrWhiteSpace(request.DefaultValue))
                    throw new PubliconException(ErrorCode.DefaultValueIsNull, "DefaultValue must be provided when making field required!");

                foreach (var publication in category.Publications)
                {
                    if(!publication.PublicationFields.Any(p => p.FieldId == field.Id))
                        publication.AddPublicationField(new PublicationField(field.Id, request.DefaultValue));
                }    
            }

            _mapper.Map(request, field);

            await _categoryRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
