using Notifications.Domain.Common;

namespace Notifications.Domain.Entity
{
    public class TradeNotification : BaseAuditableEntity
    {
        public Guid TradeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
