using AutoMapper;
using Notifications.Application.Common.Mappings;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;

namespace Notifications.Application.Notifications.Queries
{
    public class NotificationDTO : IMapFrom<Notification>
    {
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
            profile.CreateMap<Notification, NotificationDTO>();
        }
    }
}
