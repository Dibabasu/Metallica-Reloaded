using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entity;
using Notifications.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace Notifications.Infrastructure.Persistence
{
    public class NotificationsDbContext : DbContext, INotificationsDbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Notification> Notifications => Set<Notification>();

        public DbSet<TradeNotification> TradeNotifications => Set<TradeNotification>();

        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public NotificationsDbContext(
            DbContextOptions<NotificationsDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor,
            IMediator mediator)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            _mediator = mediator;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
