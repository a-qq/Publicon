using Microsoft.EntityFrameworkCore;
using Publicon.Core.DAL;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Repositories.Abstract;
using System;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Concrete
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PubliconContext context)
            : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            return await _dbSet
                .FirstOrDefaultAsync(up => up.Email == email);
        }

        public async Task<User> GetByPasswordSecurityCodeAsync(string passwordSecurityCode)
        {
            if (string.IsNullOrWhiteSpace(passwordSecurityCode))
                throw new ArgumentNullException(nameof(passwordSecurityCode));

            return await _dbSet
                .FirstOrDefaultAsync(up => up.PasswordSecurityCode == passwordSecurityCode);
        }

        public async Task<User> GetBySecurityCodeAsync(string securityCode)
        {
            if (string.IsNullOrWhiteSpace(securityCode))
                throw new ArgumentNullException(nameof(securityCode));

            return await _dbSet
                .FirstOrDefaultAsync(up => up.SecurityCode == securityCode);
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            return await _dbSet.AnyAsync(up => up.Email == email);
        }

    }
}
