using Publicon.Core.Entities.Concrete;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>, IRepository
    {
        Task<bool> ExistByEmailAsync(string email);
        Task<User> GetBySecurityCodeAsync(string securityCode);
        Task<User> GetByPasswordSecurityCodeAsync(string passwordSecurityCode);
        Task<User> GetByEmailAsync(string email);
    }
}
