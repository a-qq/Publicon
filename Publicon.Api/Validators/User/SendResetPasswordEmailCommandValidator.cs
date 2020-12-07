using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class SendResetPasswordEmailCommandValidator : AbstractValidator<SendResetPasswordEmailCommand>
    {
        public SendResetPasswordEmailCommandValidator()
        {
            RuleFor(p => p.Email).EmailAddress();
        }
    }
}
