using EventBus.RabbitMQ;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Trades.Application.Common.Behaviours;
using Trades.Application.PublishTrades;
using Trades.Application.PublishTrades.Interfaces;

namespace Trades.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddScoped<IPublishTrade, PublishTradeService>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(RabbitMQCommon.Rabbitmqhost), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQCOnfig")["userName"]);
                        h.Password(configuration.GetSection("RabbitMQCOnfig")["password"]);
                    });

                }));
            });

            return services;
        }
    }
}
