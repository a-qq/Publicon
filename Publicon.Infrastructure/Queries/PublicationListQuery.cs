using System;
using System.Collections.Generic;

namespace Publicon.Infrastructure.Queries
{
    public abstract class PublicationListQuery
    {
        public string SearchQuery { get; set; }
        public IEnumerable<Guid> CategoryIds { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
