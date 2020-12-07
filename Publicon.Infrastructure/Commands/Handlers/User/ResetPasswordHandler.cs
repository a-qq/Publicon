using MediatR;
using Microsoft.AspNetCore.Identity;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Core.Entities.Concrete.User> _passwordHasher;

        public ResetPasswordHandler(
            IUserRepository userRepository,
            IPasswordHasher<Core.Entities.Concrete.User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPasswordSecurityCodeAsync(request.SecurityCode);

            if (user == null || !user.SecurityCodeIssuedAt.HasValue)
                throw new PubliconException(ErrorCode.InvalidSecurityCode);

            if (DateTime.UtcNow.Subtract(user.SecurityCodeIssuedAt.Value) > TimeSpan.FromMinutes(double.Parse(Environment.GetEnvironmentVariable("SecurityCodeExpiryTimeInMin"))))
                throw new PubliconException(ErrorCode.ExpiredSecurityCode);

            user.ResetPassword(_passwordHasher.HashPassword(user, request.NewPassword));
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
