using Microsoft.EntityFrameworkCore;
using Trades.Domain.Entity;

namespace Trades.Application.Common.Interfaces
{
    public interface ITradeApplicationDbContext
    {
        DbSet<Trade> Trades { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}