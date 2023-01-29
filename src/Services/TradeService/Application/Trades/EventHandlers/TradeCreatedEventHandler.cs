using EventBus.RabbitMQ.Trades.Notificaitons;
using MediatR;
using Microsoft.Extensions.Logging;
using Trades.Application.PublishTrades.Interfaces;
using Trades.Domain.Events;

namespace Trades.Application.Trades.EventHandlers
{
    public class TradeCreatedEventHandler : INotificationHandler<TradeCreatedEvent>
    {
        private readonly IPublishTrade _publishTrade;
        private readonly ILogger<TradeCreatedEventHandler> _logger;

        public TradeCreatedEventHandler(IPublishTrade publishTrade, ILogger<TradeCreatedEventHandler> logger)
        {
            _publishTrade = publishTrade;
            _logger = logger;   
        }

        public async Task Handle(TradeCreatedEvent trade, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Trade Domain Event: {DomainEvent}", trade.GetType().Name);

            await _publishTrade.CreatePublishTrade(new TradesMessage
            {
                TradeId = trade.Trade.Id
            });

        }
    }
}
