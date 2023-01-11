using AutoMapper;
using Notifications.Application.Common.Mappings;
using Notifications.Domain.Common;
using Notifications.Domain.Entity;

namespace Notifications.Application.Notifications.Queries
{
    public class NotificationDTO : IMapFrom<Notification>
    {
        public Guid NotificationId { get; set; }
        public Guid TradeId { get; set; }
        public NotificaitonStatus SMSStatus { get; set; }
        public NotificaitonStatus EmailStatus { get; set; }
        public int EmailRetries { get; set; }
        public DateTime SentDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationDTO>()
                .ForMember(d => d.NotificationId, opt => opt.MapFrom(s => s.Id)); ;
        }
    }
}
