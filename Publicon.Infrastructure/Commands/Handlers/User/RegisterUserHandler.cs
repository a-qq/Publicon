using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Extensions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using Publicon.Infrastructure.Managers.Abstract;
using Publicon.Infrastructure.Settings;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly FrontendSettings _frontendSettings;
        private readonly IMailManager _mailManager;
        private readonly IPasswordHasher<Core.Entities.Concrete.User> _passwordHasher;
        private readonly ICridentialsManager _cridentialsManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserHandler(
            IOptions<FrontendSettings> frontendSettings,
            IMailManager mailManager,
            IPasswordHasher<Core.Entities.Concrete.User> passwordHasher,
            ICridentialsManager cridentialsManager,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _frontendSettings = frontendSettings.Value;
            _mailManager = mailManager;
            _passwordHasher = passwordHasher;
            _cridentialsManager = cridentialsManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.ExistByEmailAsync(request.Email))
                throw new PubliconException(ErrorCode.UserWithGivenEmailExist);

            var user = _mapper.Map<Core.Entities.Concrete.User>(request);
            user.SetSecurityCode(_cridentialsManager.GenerateSecurityCode());
            user.SetHashedPassword(_passwordHasher.HashPassword(user, request.Password));
            user.SetRole(Core.Entities.Enums.UserRole.User);

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            var link = user.GenerateActivationLink($"{_frontendSettings.Url}confirm-email");
            await _mailManager.SendMailAsync(user.Email, "Confirm your email adress!", "Your activation link: " + link);

            return Unit.Value;
        }
    }
}
