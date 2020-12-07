using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Category;

namespace Publicon.Api.Validators.Category
{
    public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
    {
        public EditCategoryCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(p => p.Description).MaximumLength(750);
            RuleFor(p => p.IsArchived).NotNull();
        }
    }
}
