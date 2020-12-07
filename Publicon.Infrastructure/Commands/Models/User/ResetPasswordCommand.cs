using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class ResetPasswordCommand : IRequest
    {
        public string SecurityCode { get; set; }
        public string NewPassword { get; set; }
    }
}
