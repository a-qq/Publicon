using Publicon.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Abstract
{
    public interface IPublicationRepository : IGenericRepository<Publication>, IRepository
    {
        // new Task<Publication> GetByIdAsync(Guid id);
        Task<Tuple<int, List<Publication>>> FilterAndSearchAsync(IEnumerable<Guid> categories, IEnumerable<Guid> users, string searchQuery, int pageNumber, int pageSize); 
    }
}
