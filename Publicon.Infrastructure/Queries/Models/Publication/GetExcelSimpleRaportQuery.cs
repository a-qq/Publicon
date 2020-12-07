using MediatR;
using System.IO;

namespace Publicon.Infrastructure.Queries.Models.Publication
{
    public class GetExcelSimpleRaportQuery : PublicationListQuery, IRequest<MemoryStream>
    {
    }
}
