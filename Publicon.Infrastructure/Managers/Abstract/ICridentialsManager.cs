using Publicon.Infrastructure.DTOs;
using System;

namespace Publicon.Infrastructure.Managers.Abstract
{
    public interface ICridentialsManager : IManager
    {
        public string GenerateSecurityCode();
        public TokenDTO GenerateToken(Guid userId, string email, string role, string givenName, string familyName);
        public Guid GetIdFromToken(string token);
        public void CompareRefreshTokens(string givenToken, string refreshToken);
    }
}
