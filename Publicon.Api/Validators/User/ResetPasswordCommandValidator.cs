using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(p => p.SecurityCode).NotNull().NotEmpty();
            RuleFor(p => p.NewPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);
        }
    }
}
