using MediatR;
using Trades.Application.Common.Exceptions;
using Trades.Application.Common.Interfaces;
using Trades.Domain.Common;

namespace Trades.Application.Trades.Commands.UpdateTrade
{
    public record UpdateTradeStausCommand : IRequest
    {
        public Guid Id { get; init; }

        public TradeStatus TradeStatus { get; init; }

        public bool Done { get; init; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTradeStausCommand>
    {
        private readonly ITradeApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(ITradeApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTradeStausCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Trades
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Trades), request.Id);
            }

            entity.TradeStatus = request.TradeStatus;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
