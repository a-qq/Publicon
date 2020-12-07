using Microsoft.EntityFrameworkCore;
using Publicon.Core.DAL;
using Publicon.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Concrete
{
    internal class GenericDeleteAbleRepository<T> : GenericRepository<T> where T : DeleteAble
    {
        public GenericDeleteAbleRepository(PubliconContext context) : base(context) { }
        public override async Task<bool> ExistAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _dbSet.AnyAsync(e => e.Id == id && !e.DeletedAt.HasValue);
        }

        public override async Task<T> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var entity = await _dbSet.FindAsync(id);

            return (entity == null || entity.DeletedAt.HasValue) ? null : entity;
        }

        public override async Task<IEnumerable<T>> GetEntitiesAsync()
            => await _dbSet.Where(e => !e.DeletedAt.HasValue).ToListAsync();

        public override async Task<IEnumerable<T>> GetEntitiesAsync(IEnumerable<Guid> entityIds)
        {
            if (entityIds == null)
                throw new ArgumentNullException(nameof(entityIds));

               var entities = await _dbSet
                    .Where(e => entityIds.Contains(e.Id) && !e.DeletedAt.HasValue)
                    .ToListAsync();

            return entities;
        }
    }
}
