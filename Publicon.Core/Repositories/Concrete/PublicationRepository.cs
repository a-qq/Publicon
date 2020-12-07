using Microsoft.EntityFrameworkCore;
using Publicon.Core.DAL;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Extensions;
using Publicon.Core.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publicon.Core.Repositories.Concrete
{
    internal class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(PubliconContext context)
            : base(context) { }

        public async Task<Tuple<int, List<Publication>>> FilterAndSearchAsync(IEnumerable<Guid> categories, IEnumerable<Guid> users, string searchQuery, int pageNumber, int pageSize)
        {
            var publications = _dbSet
                 .Include(p => p.User)
                 .Include(p => p.Category)
                 .OrderBy(p => p.AddedAt)
                 .AsQueryable();

            if (categories != null && categories.Any())
                publications = publications.Where(p => categories.Contains(p.CategoryId));

            if (users != null && users.Any())
                publications = publications.Where(p => p.UserId.HasValue && users.Contains(p.UserId.Value));

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var searchQueryLowLetters = searchQuery.ToLower();
                publications = publications.Where(p => p.Title.ToLower().Contains(searchQueryLowLetters));
            }

            var totalCount = await publications.CountAsync();
            if (pageNumber != 0 && pageSize != 0)
                publications = publications.ApplyPagination(pageNumber, pageSize);

            var publicationEntities = await publications.ToListAsync();

            return Tuple.Create(totalCount, publicationEntities);
        }

        public override async Task<Publication> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _dbSet
                .Include(p => p.PublicationFields)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
