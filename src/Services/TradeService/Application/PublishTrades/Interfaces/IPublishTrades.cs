using EventBus.RabbitMQ.Trades.Notificaitons;

namespace Trades.Application.PublishTrades.Interfaces
{
    public interface IPublishTrade
    {
        Task CreatePublishTrade(TradesMessage notificationMessage);
    }
}
