using MediatR;
using System;
using System.Collections.Generic;
using System.IO;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class GetExcelDetailedRaportQuery : IRequest<MemoryStream>
    {
        public string SearchQuery { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
