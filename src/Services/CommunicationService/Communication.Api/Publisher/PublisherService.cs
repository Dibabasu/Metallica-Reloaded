using Communications.Api.Publisher.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Notifications.Communications;
using EventBus.RabbitMQ.Queues;
using MassTransit;
using System.Text;

namespace Communications.Api.Publisher
{
    public class PublisherService : IPublisherService
    {
        private readonly IBus _bus;
        public PublisherService(IBus bus)
        {
            _bus = bus;
        }

        public async Task UpdateNotificaitonStatus(NoticationStatusMessage noticationStatusMessage)
        {
            if (noticationStatusMessage != null)
            {

                StringBuilder QueueuriBuilder = new StringBuilder();
                QueueuriBuilder.Append(RabbitMQCommon.Rabbitmqhost);
                QueueuriBuilder.Append("/");
                QueueuriBuilder.Append(RabbitMQQueue.NotificationsUpdateEventQueue);

                Uri uri = new(QueueuriBuilder.ToString());

                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(noticationStatusMessage);
            }
        }
    }
}
