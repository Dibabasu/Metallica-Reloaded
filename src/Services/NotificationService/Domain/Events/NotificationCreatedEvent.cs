using Notifications.Domain.Common;
using Notifications.Domain.Entity;

namespace Notifications.Domain.Events
{
    public class NotificationCreatedEvent :BaseEvent
    {
        public NotificationCreatedEvent(Notification notification)
        {
            Notification = notification;
        }   
        
        public Notification Notification { get; }
    }
}
