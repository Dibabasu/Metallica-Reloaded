using Trades.Application.Common.Interfaces;

namespace Trades.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
