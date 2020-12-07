using Microsoft.EntityFrameworkCore;
using Publicon.Core.DAL;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Concrete
{
    internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PubliconContext context) 
            : base (context) { }

        public override async Task<Category> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _dbSet
                .Include(c => c.Fields)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await _dbSet.AnyAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> FilterAsync(bool? isArchived)
        {
            var categories = _dbSet.AsQueryable();

            if (isArchived != null)
                categories = categories.Where(c => c.IsArchived == isArchived.Value);

            return await categories
                .Include(c => c.Fields)
                .ToListAsync();
        }

        //public async Task<Field> GetFieldAsync(Guid categoryId, Guid fieldId)
        //{
        //    if (categoryId == Guid.Empty)
        //        throw new ArgumentNullException(nameof(categoryId));

        //    if (fieldId == Guid.Empty)
        //        throw new ArgumentNullException(nameof(fieldId));

        //    return await _dbSet.Include(c => c.Fields)
        //        .Where(c => c.Id == categoryId)
        //        .Select(c => c.Fields.FirstOrDefault(f => f.Id == fieldId))
        //        .FirstOrDefaultAsync();
        //}
    }
}
