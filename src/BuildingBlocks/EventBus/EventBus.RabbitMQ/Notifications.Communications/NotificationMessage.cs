namespace EventBus.RabbitMQ.Notifications.Communications
{
    public class NotificationMessage
    {
        public Guid NotificationId { get; set; }
        public Guid TradeId { get; set; }
    }
}