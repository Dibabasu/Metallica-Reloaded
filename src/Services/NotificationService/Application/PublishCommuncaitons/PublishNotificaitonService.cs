using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Notifications.Communications;
using EventBus.RabbitMQ.Queues;
using MassTransit;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using System.Text;

namespace Notifications.Application.PublishCommuncaitons
{
    public class PublishNotificaitonService : IPublishNotification
    {
        private readonly IBus _bus;
        public PublishNotificaitonService(IBus bus)
        {
            _bus = bus;
        }
        public async Task CreateNotificaiton(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null)
            {

                StringBuilder QueueuriBuilder = new StringBuilder();
                QueueuriBuilder.Append(RabbitMQCommon.Rabbitmqhost);
                QueueuriBuilder.Append("/");
                QueueuriBuilder.Append(RabbitMQQueue.NotificationsEventQueue);

                Uri uri = new(QueueuriBuilder.ToString());

                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(notificationMessage);
            }
        }
    }
}
