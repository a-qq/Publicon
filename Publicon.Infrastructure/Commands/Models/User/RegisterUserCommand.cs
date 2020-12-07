using MediatR;

namespace Publicon.Infrastructure.Commands.Models.User
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Password { get; set; }
    }
}
