using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(p => p.Email)
                .EmailAddress();

            RuleFor(p => p.FamilyName)
                .NotEmpty()
                .NotNull()
                .Length(3, 200);

            RuleFor(p => p.GivenName)
                .NotEmpty()
                .NotNull()
                .Length(3, 200);

            RuleFor(p => p.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);
        }
    }
}
