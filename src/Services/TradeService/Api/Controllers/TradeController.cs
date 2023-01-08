using Microsoft.AspNetCore.Mvc;
using Trades.Application.Common.Models;
using Trades.Application.Trades.Commands.CreateTrade;
using Trades.Application.Trades.Commands.UpdateTrade;
using Trades.Application.Trades.Queries;
using Trades.Application.Trades.Queries.GetTradeById;
using Trades.Application.Trades.Queries.GetTradesWithPagination;

namespace Trades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<TradeDTO>>> GetTradesWithPagination([FromQuery] GetTradesWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TradeDTO>> GetTradeById(Guid id)
        {
            return await Mediator.Send(new GetTradebyIdQuery
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTradeCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTradeStausCommand command)
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
