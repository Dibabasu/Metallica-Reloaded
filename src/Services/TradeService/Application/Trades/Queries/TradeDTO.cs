using AutoMapper;
using Trades.Application.Common.Mappings;
using Trades.Domain.Common;
using Trades.Domain.Entity;

namespace Trades.Application.Trades.Queries
{
    public class TradeDTO : IMapFrom<Trade>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Trade, TradeDTO>()
                .ForMember(d => d.TradeId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
