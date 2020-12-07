using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Api.Validators.Publication
{
    public class CreatePublicationCommandValidator : AbstractValidator<CreatePublicationCommand>
    {
        public CreatePublicationCommandValidator()
        {
            RuleFor(p => p.CategoryId).NotNull().NotEmpty();
            RuleFor(p => p.Title).NotNull().NotEmpty().MaximumLength(300);
            RuleFor(p => p.PublicationTime).NotNull().NotEmpty();
            When(p => p.File != null, () =>
                {
                    RuleFor(x => x.File).Must(y => y.Length < 78643200).WithMessage("Max allowed file size is 75 MB");
                });
            
            RuleForEach(p => p.PublicationFields)
                .ChildRules(p =>
                {
                    p.RuleFor(x => x.FieldId).NotNull().NotEmpty();
                    p.RuleFor(x => x.Value).NotNull();
                });
            RuleFor(p => p.PublicationFields).SetValidator(new UniqueFieldIdValidator<PublicationFieldToManipulateDTO>());
        }
    }
}
