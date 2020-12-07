using FluentValidation;
using Publicon.Infrastructure.Commands.Models.Category;

namespace Publicon.Api.Validators.Category
{
    public class CreateFieldCommandValidator : AbstractValidator<CreateFieldCommand>
    {
        public CreateFieldCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(200);
            RuleFor(p => p.IsRequired).NotNull();
            When(p => p.IsRequired, () =>
            {
                RuleFor(p => p.DefaultValue).NotNull().NotEmpty();
            });
        }
    }
}
