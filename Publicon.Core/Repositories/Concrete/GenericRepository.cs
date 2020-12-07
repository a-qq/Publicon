using Microsoft.EntityFrameworkCore;
using Publicon.Core.DAL;
using Publicon.Core.Entities.Abstract;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Concrete
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly PubliconContext _context;
        protected readonly DbSet<T> _dbSet;


        public GenericRepository(PubliconContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetEntitiesAsync() 
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetEntitiesAsync(IEnumerable<Guid> entityIds)
        {
            if (entityIds == null)
                throw new ArgumentNullException(nameof(entityIds));

            return await _dbSet.Where(e => entityIds.Contains(e.Id))
                .ToListAsync();
        }

        public IQueryable<T> GetQueryable()
             => _dbSet.AsQueryable();

        public async Task SaveChangesAsync()
        {
            if (!(await _context.SaveChangesAsync() >= 0)) //can this even return negative value? maybe better try & catch?
                throw new PubliconException(ErrorCode.DbSavingException);
        }
    }
}
