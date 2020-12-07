using MediatR;
using Microsoft.Extensions.Hosting;
using Publicon.Infrastructure.Commands.Models.Notification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Api.HostedServices
{
    public class SendNotificationService : IHostedService
    {
        private readonly IMediator _mediator;
        private Timer _timer;

        public SendNotificationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            TimeSpan interval = TimeSpan.FromHours(1);
            var curTime = DateTime.UtcNow;
            var nextRunTime = DateTime.UtcNow.Date.AddHours(curTime.Hour + 1).AddMinutes(1);
            var firstInterval = nextRunTime.Subtract(curTime);

            Action action = () =>
            {
                var t1 = Task.Delay(firstInterval);
                t1.Wait();
                SendNotifications(null);

                _timer = new Timer(
                    SendNotifications,
                    null,
                    TimeSpan.Zero,
                    interval
                );
            };
            Task.Run(action);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private  void SendNotifications(object state)
        {
            _mediator.Send(new SendNotificationCommand());
        }
    }
}
