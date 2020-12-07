using MediatR;
using Microsoft.AspNetCore.Identity;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Entities.Enums;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, TokenDTO>
    {
        private readonly ICridentialsManager _cridentialsManager;
        private readonly IPasswordHasher<Core.Entities.Concrete.User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(
            ICridentialsManager cridentialsManager,
            IPasswordHasher<Core.Entities.Concrete.User> passwordHasher,
            IUserRepository userRepository)
        {
            _cridentialsManager = cridentialsManager;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<TokenDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new PubliconException(ErrorCode.UserWithGivenEmailNotExist);

            if (!user.IsActive)
                throw new PubliconException(ErrorCode.UnverifiedEmail);

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, request.Password);
            if(verificationResult < PasswordVerificationResult.Success)
                throw new PubliconException(ErrorCode.InvalidPassword);

            var userRole = Enum.GetName(typeof(UserRole), user.Role);
            var tokenDTO = _cridentialsManager.GenerateToken(user.Id, user.Email, userRole, user.GivenName, user.FamilyName);
            
            user.SetRefreshToken(tokenDTO.RefreshToken);
            await _userRepository.SaveChangesAsync();

            return tokenDTO;

        }
    }
}
