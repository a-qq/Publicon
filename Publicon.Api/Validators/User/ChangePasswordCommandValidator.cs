using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(p => p.OldPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);

            RuleFor(p => p.NewPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);
        }
    }
}
