using MediatR;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class LoginUserCommand : IRequest<TokenDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
