using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Queues;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Common.Behaviours;
using Notifications.Application.Consumer;
using Notifications.Application.PublishCommuncaitons;
using Notifications.Application.PublishCommuncaitons.Interfaces;
using System.Reflection;

namespace Notifications.Application
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


            services.AddScoped<IPublishNotification, PublishNotificaitonService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificaitonStatusConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(RabbitMQCommon.Rabbitmqhost), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQCOnfig")["userName"]);
                        h.Password(configuration.GetSection("RabbitMQCOnfig")["password"]);
                    });

                    config.ReceiveEndpoint(RabbitMQQueue.NotificationsUpdateEventQueue, ep =>
                    {
                        ep.PrefetchCount = 16;

                        ep.ConfigureConsumer<NotificaitonStatusConsumer>(provider);

                    });
                }));
            });

            return services;
        }
    }
}
