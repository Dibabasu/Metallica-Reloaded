using Trades.Domain.Common;

namespace Trades.Domain.Entity
{
    public class Trade : BaseAuditableEntity
    {
        public Side Side { get; set; }
        public TradeStatus TradeStatus { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime TradeDate { get; set; }
        public string CommoditiesIdentifier { get; set; } = string.Empty;
        public string CounterpartiesIdentifier { get; set; } = string.Empty;
        public string LocationIdentifier { get; set; } = string.Empty;
        public bool IsSubmitted { get; set; }

    }
}
