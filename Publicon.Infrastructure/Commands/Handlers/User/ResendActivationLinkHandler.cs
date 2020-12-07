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
    public class ResendActivationLinkHandler : IRequestHandler<ResendActivationLinkCommand, Unit>
    {
        private readonly FrontendSettings _frontendSettings;
        private readonly IMailManager _mailManager;
        private readonly ICridentialsManager _cridentialsManager;
        private readonly IUserRepository _userRepository;

        public ResendActivationLinkHandler(
            IOptions<FrontendSettings> frontendSettings,
            IMailManager mailManager,
            ICridentialsManager cridentialsManager,
            IUserRepository userRepository)
        {
            _frontendSettings = frontendSettings.Value;
            _mailManager = mailManager;
            _cridentialsManager = cridentialsManager;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ResendActivationLinkCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new PubliconException(ErrorCode.UserWithGivenEmailNotExist);

            if (user.IsActive)
                throw new PubliconException(ErrorCode.EmailAlreadyVerified);

            if (user.SecurityCodeIssuedAt.HasValue && DateTime.UtcNow.Subtract(user.SecurityCodeIssuedAt.Value) 
                < TimeSpan.FromMinutes(double.Parse(Environment.GetEnvironmentVariable("AntySpamTimeSpanInMin"))))
                    throw new PubliconException(ErrorCode.AntySpamTryAgainLater);

            user.SetSecurityCode(_cridentialsManager.GenerateSecurityCode());
            await _userRepository.SaveChangesAsync();

            var link = user.GenerateActivationLink($"{_frontendSettings.Url}confirm-email");
            await _mailManager.SendMailAsync(user.Email, "Confirm your email adress!", "Your activation link: " + link);

            return Unit.Value;
        }
    }
}
