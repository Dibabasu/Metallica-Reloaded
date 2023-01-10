using Communications.Api.Model.Common;

namespace Communications.Api.Model
{
    public class TradeDTO
    {
        public Guid TradeId { get; set; }
        public Side Side { get; set; }
        public TradeStatus TradeStatus { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public DateTime TradeDate { get; set; }

        public string CommoditiesIdentifier { get; set; }

        public string CounterpartiesIdentifier { get; set; }

        public string LocationIdentifier { get; set; }
    }
}
