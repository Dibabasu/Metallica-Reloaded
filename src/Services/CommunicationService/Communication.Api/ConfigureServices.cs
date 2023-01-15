using Communications.Api.Publisher.Interfaces;
using Communications.Api.Publisher;
using Communications.Api.Services.Interfaces;
using Communications.Api.Services;
using MassTransit;
using Communications.Api.Consumers;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Queues;

namespace Communications.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(RabbitMQCommon.Rabbitmqhost), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQCOnfig")["userName"]);
                        h.Password(configuration.GetSection("RabbitMQCOnfig")["password"]);
                    });
                    cfg.ReceiveEndpoint(RabbitMQQueue.NotificationsEventQueue, ep =>
                    {
                        ep.PrefetchCount = 16;

                        ep.ConfigureConsumer<NotificationConsumer>(provider);

                    });

                }));
            });

            services.AddHttpClient();
            
            services.AddScoped<ICommuncations, CommunicationsService>();
            services.AddScoped<ITradeDetails, TradeDetailsService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IRetryCommunication, RetryCommunicationService>();

            return services;
        }
    }
}
