using MediatR;
using System;

namespace Publicon.Infrastructure.Commands.Models.Notification
{
    public class SetNotificationCommand : IRequest
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime SendTimeInUTC { get; set; }
    }
}
