using Publicon.Core.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>, IRepository
    {
        //new Task<Category> GetByIdAsync(Guid id);
        Task<bool> ExistByNameAsync(string name);
        Task<IEnumerable<Category>> FilterAsync(bool? isArchived);
        //Task<Field> GetFieldAsync(Guid categoryId, Guid fieldId);
    }
}
