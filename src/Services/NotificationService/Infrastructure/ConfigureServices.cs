using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Common.Interfaces;
using Notifications.Infrastructure.Persistence;
using Notifications.Infrastructure.Persistence.Interceptors;
using Notifications.Infrastructure.Services;

namespace Notifications.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();


            services.AddDbContext<NotificationsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDbContext"),
                    builder => builder.MigrationsAssembly(typeof(NotificationsDbContext).Assembly.FullName)));

            services.AddScoped<INotificationsDbContext>(provider => provider.GetRequiredService<NotificationsDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

            return services;

        }
    }
}
