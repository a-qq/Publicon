using Publicon.Core.DAL;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Repositories.Abstract;

namespace Publicon.Core.Repositories.Concrete
{
    internal class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(PubliconContext context)
            : base(context) { }

    }
}
