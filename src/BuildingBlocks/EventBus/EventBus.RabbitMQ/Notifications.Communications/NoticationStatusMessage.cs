namespace EventBus.RabbitMQ.Notifications.Communications
{
    public class NoticationStatusMessage
    {
        public Guid NotificaitonId { get; set; }
        public int EmailStatus { get; set; }
        public int SMSStatus { get; set; }
        public int NumberOfRetries { get; set; } = 0;
    }
}
