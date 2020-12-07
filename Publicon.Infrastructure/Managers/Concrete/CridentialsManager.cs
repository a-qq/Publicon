using Microsoft.IdentityModel.Tokens;
using Publicon.Core.Exceptions;
using Publicon.Core.Extensions;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using Publicon.Infrastructure.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Publicon.Infrastructure.Managers.Concrete
{
    internal class CridentialsManager : ICridentialsManager
    {
        private readonly JwtSettings _jwtSettings;

        public CridentialsManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string GenerateSecurityCode()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var securityCodeData = new byte[128];
                randomNumberGenerator.GetBytes(securityCodeData);
                return Convert.ToBase64String(securityCodeData);
            }
        }

        public TokenDTO GenerateToken(Guid userId, string email, string role, string givenName, string familyName)
        {
            var secretKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_jwtSettings.ExpiryTime);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.GivenName, givenName),
                new Claim(JwtRegisteredClaimNames.FamilyName, familyName),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToBinary().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", role)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);
            

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = GenerateRefreshToken();

            var tokenDto = new TokenDTO()
            {
                Token = token,
                RefreshToken = refreshToken
            };

            return tokenDto;
        }

        public Guid GetIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                    },
                    out var validatedToken);

                var userId = principal.GetUserId();

                return userId;
            }
            catch (Exception ex)
            {
                throw new PubliconException(ErrorCode.InvalidCredentials, "Invalid credentials", ex);
            }
        }

        public void CompareRefreshTokens(string givenToken, string refreshToken)
        {
            if (givenToken != refreshToken)
            {
                throw new PubliconException(ErrorCode.InvalidRefreshToken);
            }
        }

        private static string GenerateRefreshToken()
        {
            var number = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
