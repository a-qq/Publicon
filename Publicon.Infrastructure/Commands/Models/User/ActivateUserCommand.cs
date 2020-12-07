using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class ActivateUserCommand : IRequest
    {
        public string SecurityCode { get; set; }
    }
}
