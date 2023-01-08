using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entity;

namespace Notifications.Application.Common.Interfaces
{
    public interface INotificationsDbContext
    {
        DbSet<Notification> Notifications { get; }
        DbSet<TradeNotification> TradeNotifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}