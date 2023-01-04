using EventBus.RabbitMQ.Notifications.Communications;

namespace Communications.Api.Publisher.Interfaces
{
    public interface IPublisherService
    {
        Task UpdateNotificaitonStatus(NoticationStatusMessage noticationStatusMessage);
    }
}
