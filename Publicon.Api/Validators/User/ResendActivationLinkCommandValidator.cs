using FluentValidation;
using Publicon.Infrastructure.Commands.Models.User;

namespace Publicon.Api.Validators.User
{
    public class ResendActivationLinkCommandValidator : AbstractValidator<ResendActivationLinkCommand>
    {
        public ResendActivationLinkCommandValidator()
        {
            RuleFor(p => p.Email).EmailAddress();
        }
    }
}
