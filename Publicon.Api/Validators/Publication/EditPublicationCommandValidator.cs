using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Api.Validators.Publication
{
    public class EditPublicationCommandValidator : AbstractValidator<EditPublicationCommand>
    {
        public EditPublicationCommandValidator()
        {
            RuleFor(p => p.Title).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(p => p.PublicationTime).NotNull().NotEmpty();
                
            RuleForEach(p => p.PublicationFields).ChildRules(p =>
            {
                p.RuleFor(x => x.FieldId).NotNull().NotEmpty();
                p.RuleFor(x => x.Value).NotNull();
            });
            RuleFor(p => p.PublicationFields).SetValidator(new UniqueFieldIdValidator<PublicationFieldToManipulateDTO>());
        }
    }
}
