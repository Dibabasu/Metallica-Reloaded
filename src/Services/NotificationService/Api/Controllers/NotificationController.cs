using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Notifications.Commands.CreateNotification;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.Notifications.Queries;

namespace Notifications.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ApiControllerBase
    {

        [HttpPost("save")]
        public async Task<ActionResult<Guid>> Create(CreateNotificaitonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guid>> GetNotificationByID(CreateNotificaitonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<Guid>> GetNotificationWithPagination(CreateNotificaitonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateNotificationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
