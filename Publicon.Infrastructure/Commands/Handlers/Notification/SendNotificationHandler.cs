using MediatR;
using Microsoft.EntityFrameworkCore;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Notification;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Notification
{
    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepostiory;
        private readonly IMailManager _mailManager;

        public SendNotificationHandler(
            IUserRepository userRepository,
            INotificationRepository notificationRepostiory,
            IMailManager mailManager)
        {
            _userRepository = userRepository;
            _notificationRepostiory = notificationRepostiory;
            _mailManager = mailManager;
        }

        public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepostiory.GetQueryable()
                .Where(n => n.SendTime < DateTime.UtcNow)
                .ToListAsync();

            if (notifications != null && notifications.Any())
            {
                var userEmails = await _userRepository.GetQueryable()
                    .Where(u => u.IsActive)
                    .Select(u => u.Email)
                    .ToListAsync();

                foreach (var notification in notifications)
                {
                    foreach (var email in userEmails)
                    {
                        await _mailManager.SendMailAsync(email, notification.Title, notification.Message);
                    }

                    _notificationRepostiory.Delete(notification);
                }

                await _notificationRepostiory.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
