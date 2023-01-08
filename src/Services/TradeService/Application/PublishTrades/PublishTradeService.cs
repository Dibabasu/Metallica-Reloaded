using EventBus.RabbitMQ.Queues;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Trades.Notificaitons;
using MassTransit;
using System.Text;
using Trades.Application.PublishTrades.Interfaces;

namespace Trades.Application.PublishTrades
{
    public class PublishTradeService : IPublishTrade
    {
        private readonly IBus _bus;

        public PublishTradeService(IBus bus)
        {
            _bus = bus;
        }

        public async Task CreatePublishTrade(TradesMessage tradesMessage)
        {
            if (tradesMessage != null)
            {
                StringBuilder QueueuriBuilder = new StringBuilder();
                QueueuriBuilder.Append(RabbitMQCommon.Rabbitmqhost);
                QueueuriBuilder.Append("/");
                QueueuriBuilder.Append(RabbitMQQueue.TradesEventQueue);

                Uri uri = new(QueueuriBuilder.ToString());

                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(tradesMessage);
            }
        }
    }
}
