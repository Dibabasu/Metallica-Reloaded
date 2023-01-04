using Notifications.Domain.Common;

namespace Notifications.Domain.Entity
{
    public class TradeNotification
    {
        public Guid Id { get; set; }
        public Guid TradeId { get; set; }
        public Side Side { get; set; }
        public TradeStatus TradeStatus { get; set; }
        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime TradeDate { get; set; }

        public string CommoditiesIdentifier { get; set; } = string.Empty;

        public string CounterpartiesIdentifier { get; set; } = string.Empty;

        public string LocationIdentifier { get; set; } = string.Empty;

        public bool NotificaitonProccessed { get; set; }
    }
}
