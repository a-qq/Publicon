using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);
        }
    }
}
