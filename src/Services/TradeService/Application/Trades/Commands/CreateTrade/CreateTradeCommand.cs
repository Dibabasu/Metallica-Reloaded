using MediatR;
using Trades.Application.Common.Interfaces;
using Trades.Application.PublishTrades.Interfaces;
using Trades.Domain.Common;
using Trades.Domain.Entity;
using Trades.Domain.Events;

namespace Trades.Application.Trades.Commands.CreateTrade
{
    public class CreateTradeCommand : IRequest<Guid>
    {
        public Side Side { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? CommoditiesIdentifier { get; set; }
        public string? CounterpartiesIdentifier { get; set; }
        public string? LocationIdentifier { get; set; }
    }
    public class CreateTradeCommandHandler : IRequestHandler<CreateTradeCommand, Guid>
    {
        private readonly ITradeApplicationDbContext _context;
        public CreateTradeCommandHandler(ITradeApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateTradeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Trade
            {
                CommoditiesIdentifier = request.CommoditiesIdentifier,
                CounterpartiesIdentifier = request.CounterpartiesIdentifier,
                LocationIdentifier = request.LocationIdentifier,
                Price = request.Price,
                Quantity = request.Quantity,
                Side = request.Side,
                TradeDate = DateTime.UtcNow,
                TradeStatus = TradeStatus.OPEN,
                IsSubmitted = false
            };
            _context.Trades.Add(entity);

            entity.AddDomainEvent(new TradeCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);



            return entity.Id;
        }
    }
}
