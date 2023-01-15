using MediatR;
using Microsoft.EntityFrameworkCore;
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

    public class UpdateTradeCommandHandler : IRequestHandler<UpdateTradeStausCommand>
    {
        private readonly ITradeApplicationDbContext _context;

        public UpdateTradeCommandHandler(ITradeApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTradeStausCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Trades
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

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
