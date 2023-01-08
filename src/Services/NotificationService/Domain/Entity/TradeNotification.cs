using Notifications.Domain.Common;

namespace Notifications.Domain.Entity
{
    public class TradeNotification : BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid TradeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
