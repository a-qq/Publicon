using MediatR;
using Microsoft.Extensions.Options;
using Publicon.Core.Exceptions;
using Publicon.Core.Extensions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using Publicon.Infrastructure.Managers.Abstract;
using Publicon.Infrastructure.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class SendResetPasswordEmailHandler : IRequestHandler<SendResetPasswordEmailCommand, Unit>
    {
        private readonly IMailManager _mailManager;
        private readonly FrontendSettings _frontendSettings;
        private readonly IUserRepository _userRepository;
        private readonly ICridentialsManager _cridentialsManager;

        public SendResetPasswordEmailHandler(
            IMailManager mailManager,
            IOptions<FrontendSettings> frontendSettings,
            IUserRepository userRepository,
            ICridentialsManager cridentialsManager)
        {
            _mailManager = mailManager;
            _frontendSettings = frontendSettings.Value;
            _userRepository = userRepository;
            _cridentialsManager = cridentialsManager;
        }

        public async Task<Unit> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new PubliconException(ErrorCode.UserWithGivenEmailNotExist);

            if (!user.IsActive)
                throw new PubliconException(ErrorCode.UnverifiedEmail);

            if (user.SecurityCodeIssuedAt.HasValue && DateTime.UtcNow.Subtract(user.SecurityCodeIssuedAt.Value) 
                 < TimeSpan.FromMinutes(double.Parse(Environment.GetEnvironmentVariable("AntySpamTimeSpanInMin"))))
                    throw new PubliconException(ErrorCode.AntySpamTryAgainLater);
            
            user.SetPasswordSecurityCode(_cridentialsManager.GenerateSecurityCode());
            await _userRepository.SaveChangesAsync();

            var link = user.GenerateResetPasswordLink($"{_frontendSettings.Url}reset-password");
            await _mailManager.SendMailAsync(user.Email, "Reset your password!", "Your reset password link: " + link);

            return Unit.Value;
        }
    }
}
