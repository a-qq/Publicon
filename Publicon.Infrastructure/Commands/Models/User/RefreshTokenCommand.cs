using MediatR;
using Publicon.Infrastructure.DTOs;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class RefreshTokenCommand : Command, IRequest<TokenDTO>
    {
        public string RefreshToken { get; set; }
    }
}
