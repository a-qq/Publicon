using Publicon.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<bool> ExistAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetEntitiesAsync();
        Task<IEnumerable<T>> GetEntitiesAsync(IEnumerable<Guid> entityIds);
        IQueryable<T> GetQueryable();
        void Add(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
