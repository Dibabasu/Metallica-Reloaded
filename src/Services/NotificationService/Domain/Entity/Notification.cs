using Notifications.Domain.Common;

namespace Notifications.Domain.Entity
{
    public class Notification : BaseAuditableEntity
    {

        public Guid TradeId { get; set; }
        public NotificaitonStatus SMSStatus { get; set; }
        public NotificaitonStatus EmailStatus { get; set; }
        public int EmailRetries { get; set; }
        public DateTime SentDate { get; set; }

    }
}
