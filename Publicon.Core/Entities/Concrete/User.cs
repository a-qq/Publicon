using Publicon.Core.Entities.Abstract;
using Publicon.Core.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Publicon.Core.Entities.Concrete
{
    public class User : Entity
    {
        private readonly List<Publication> _publications = new List<Publication>();

        public UserRole Role { get; protected set; }
        public string Email { get; protected set; }
        public string GivenName { get; protected set; }
        public string FamilyName { get; protected set; }
        public string HashedPassword { get; protected set; }
        public string RefreshToken { get; protected set; }
        public string SecurityCode { get; protected set; }
        public string PasswordSecurityCode { get; protected set; }
        public DateTime? SecurityCodeIssuedAt { get; protected set; }
        public bool IsActive { get; protected set; }

        public virtual IReadOnlyCollection<Publication> Publications => _publications.AsReadOnly();

        public User(string email, string givenName, string familyName)
        {
            Email = email;
            GivenName = givenName;
            FamilyName = familyName;
            Role = UserRole.Administrator;
        }

        public void SetRole(UserRole role)
            => Role = role;

        public void SetPasswordSecurityCode(string passwordSecurityCode)
        {
            PasswordSecurityCode = passwordSecurityCode;
            SecurityCodeIssuedAt = DateTime.UtcNow;
        }

        public void ResetPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
            PasswordSecurityCode = null;
            SecurityCodeIssuedAt = null;
        }

        public void SetSecurityCode(string securityCode)
        {
            SecurityCode = securityCode;
            SecurityCodeIssuedAt = DateTime.UtcNow;
        }

        public void SetHashedPassword(string hashedPassword)
            => HashedPassword = hashedPassword;

        public void SetRefreshToken(string refreshToken)
            => RefreshToken = refreshToken;

        public void ActivateAccount()
        {
            IsActive = true;
            SecurityCode = null;
            SecurityCodeIssuedAt = null;
        }
    }
}
