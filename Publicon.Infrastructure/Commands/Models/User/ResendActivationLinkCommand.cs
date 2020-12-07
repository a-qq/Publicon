using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class ResendActivationLinkCommand : IRequest
    {
        public string Email { get; set; }

        public ResendActivationLinkCommand(string email)
            => Email = email;
    }
}
