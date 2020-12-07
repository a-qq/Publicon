using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Api.Validators.Category
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(p => p.Description).MaximumLength(750);
            RuleForEach(p => p.Fields).ChildRules(p =>
            {
                p.RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(200);
                p.RuleFor(x => x.IsRequired).NotNull();
            });
            RuleFor(p => p.Fields).SetValidator(new UniqueFieldNameValidator<FieldToAddDTO>());
        }
    }
}
