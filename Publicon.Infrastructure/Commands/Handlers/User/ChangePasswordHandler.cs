using MediatR;
using Microsoft.AspNetCore.Identity;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IPasswordHasher<Core.Entities.Concrete.User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(
            IPasswordHasher<Core.Entities.Concrete.User> passwordHasher,
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new PubliconException(ErrorCode.EntityNotExist(typeof(Core.Entities.Concrete.User)));

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, request.OldPassword);
            if (verificationResult < PasswordVerificationResult.Success)
                throw new PubliconException(ErrorCode.InvalidPassword);

            user.SetHashedPassword(_passwordHasher.HashPassword(user, request.NewPassword));
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
