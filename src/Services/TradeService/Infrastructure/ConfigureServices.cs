using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trades.Application.Common.Interfaces;
using Trades.Infrastructure.Persistence;
using Trades.Infrastructure.Persistence.Interceptors;
using Trades.Infrastructure.Services;

namespace Trades.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();


            services.AddDbContext<TradeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDbContext"),
                    builder => builder.MigrationsAssembly(typeof(TradeDbContext).Assembly.FullName)));

            services.AddScoped<ITradeApplicationDbContext>(provider => provider.GetRequiredService<TradeDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

            return services;

        }
    }
}
