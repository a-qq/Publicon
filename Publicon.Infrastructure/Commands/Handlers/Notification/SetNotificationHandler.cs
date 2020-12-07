using AutoMapper;
using MediatR;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Notification;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Notification
{
    public class SetNotificationHandler : IRequestHandler<SetNotificationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;

        public SetNotificationHandler(
            IMapper mapper,
            INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(SetNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Core.Entities.Concrete.Notification>(request);
            _notificationRepository.Add(notification);
            await _notificationRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
