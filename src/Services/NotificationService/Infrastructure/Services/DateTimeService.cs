using Notifications.Application.Common.Interfaces;

namespace Notifications.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
