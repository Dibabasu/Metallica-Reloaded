using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entity;

namespace Notifications.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Notification> Notifications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}