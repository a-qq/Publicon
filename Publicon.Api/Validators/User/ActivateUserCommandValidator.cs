using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(p => p.SecurityCode).NotNull().NotEmpty();
        }
    }
}
