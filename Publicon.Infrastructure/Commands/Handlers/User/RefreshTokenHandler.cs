using MediatR;
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
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokenDTO>
    {
        private readonly ICridentialsManager _tokenManager;
        private readonly IUserRepository _userRepository;

        public RefreshTokenHandler(
            ICridentialsManager tokenManager,
            IUserRepository userRepository)
        {
            _tokenManager = tokenManager;
            _userRepository = userRepository;
        }

        public async Task<TokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new PubliconException(ErrorCode.EntityNotExist(typeof(Core.Entities.Concrete.User)));

            _tokenManager.CompareRefreshTokens(request.RefreshToken, user.RefreshToken);

            var userRole = Enum.GetName(typeof(UserRole), user.Role);
            var tokenDTO = _tokenManager.GenerateToken(user.Id, user.Email, userRole, user.GivenName, user.FamilyName);

            user.SetRefreshToken(tokenDTO.RefreshToken);
            await _userRepository.SaveChangesAsync();

            return tokenDTO;
        }
    }
}
