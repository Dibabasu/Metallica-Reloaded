using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Common.Models;
using Notifications.Application.Notifications.Commands.CreateNotification;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Application.Notifications.Queries;
using Notifications.Application.Notifications.Queries.GetNotificationById;
using Notifications.Application.Notifications.Queries.GetNotificationsWithPagination;

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
        public async Task<ActionResult<NotificationDTO>> GetNotificationByID(Guid id)
        {
            var query = new GetNotificaitonbyIdQuery
            {
                Id = id
            };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<NotificationDTO>>> GetNotificationWithPagination([FromQuery] GetNotificationsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
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
