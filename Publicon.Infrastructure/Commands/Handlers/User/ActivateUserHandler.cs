using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public ActivateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySecurityCodeAsync(request.SecurityCode);

            if (user == null || !user.SecurityCodeIssuedAt.HasValue)
                throw new PubliconException(ErrorCode.InvalidSecurityCode);

            if (DateTime.UtcNow.Subtract(user.SecurityCodeIssuedAt.Value) > TimeSpan.FromMinutes(double.Parse(Environment.GetEnvironmentVariable("SecurityCodeExpiryTimeInMin"))))
                throw new PubliconException(ErrorCode.ExpiredSecurityCode);

            user.ActivateAccount();
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
