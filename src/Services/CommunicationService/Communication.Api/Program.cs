using Communications.Api.Consumers;
using Communications.Api.Publisher;
using Communications.Api.Publisher.Interfaces;
using Communications.Api.Services;
using Communications.Api.Services.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Queues;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri(RabbitMQCommon.Rabbitmqhost), h =>
        {
            h.Username(builder.Configuration.GetSection("RabbitMQCOnfig")["userName"]);
            h.Password(builder.Configuration.GetSection("RabbitMQCOnfig")["password"]);
        });
        cfg.ReceiveEndpoint(RabbitMQQueue.NotificationsEventQueue, ep =>
        {
            ep.PrefetchCount = 16;

            ep.ConfigureConsumer<NotificationConsumer>(provider);

        });

    }));
});

builder.Services.AddScoped<ICommuncations, CommunicationsService>();
builder.Services.AddScoped<ITradeDetails, TradeDetailsService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
