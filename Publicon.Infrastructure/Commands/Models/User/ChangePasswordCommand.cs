using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class ChangePasswordCommand : Command, IRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
