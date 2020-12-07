using Publicon.Core.Entities.Abstract;
using System.Linq;

namespace Publicon.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : Entity
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
