using System;

namespace Publicon.Infrastructure.Helpers
{
    public class PaginationMetadata
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationMetadata(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (pageNumber == 0 && pageSize ==0) ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
