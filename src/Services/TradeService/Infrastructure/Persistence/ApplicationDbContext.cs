using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trades.Application.Common.Interfaces;
using Trades.Domain.Entity;
using Trades.Infrastructure.Persistence.Interceptors;

namespace Trades.Infrastructure.Persistence
{
    public class TradeDbContext : DbContext, ITradeApplicationDbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Trade> Trades => Set<Trade>();

        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public TradeDbContext(
            DbContextOptions<TradeDbContext> options,
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
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
