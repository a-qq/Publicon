using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Category;

namespace Publicon.Api.Validators.Category
{
    public class EditFieldCommandValidator : AbstractValidator<EditFieldCommand>
    {
        public EditFieldCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(p => p.IsRequired).NotNull();
        }
    }
}
