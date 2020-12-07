using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(p => p.RefreshToken)
                .NotEmpty()
                .NotNull();
        }
    }
}
