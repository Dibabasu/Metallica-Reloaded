using EventBus.RabbitMQ.Notifications.Communications;

namespace Notifications.Application.PublishCommuncaitons.Interfaces
{
    public interface IPublishNotification
    {
        Task CreateNotificaiton(NotificationMessage notificationMessage);
    }
}
