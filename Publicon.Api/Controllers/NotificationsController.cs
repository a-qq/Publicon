using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicon.Infrastructure.Commands.Models.Notification;
using System.Threading.Tasks;

namespace Publicon.Api.Controllers
{
    [ApiController]
    [Route("api/notifications/")]
    public class NotificationsController : AbstractController
    {
        public NotificationsController(IMediator mediator) : base(mediator) { }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> SetNotification(SetNotificationCommand command)
        {
            await Handle(command);
            return NoContent();
        }

    }
}
