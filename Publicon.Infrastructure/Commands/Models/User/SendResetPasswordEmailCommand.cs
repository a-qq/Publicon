using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class SendResetPasswordEmailCommand : IRequest
    {
        public string Email { get; set; }

        public SendResetPasswordEmailCommand(string email)
            => Email = email;
    }
}
