using EventBus.RabbitMQ.Trades.Notificaitons;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entity;

namespace Notifications.Application.Consumer
{
    public class TradeConsumer : IConsumer<TradesMessage>
    {
        private readonly ILogger<TradeConsumer> _logger;
        private readonly INotificationsDbContext _context;

        public TradeConsumer(ILogger<TradeConsumer> logger, INotificationsDbContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }

        public async Task Consume(ConsumeContext<TradesMessage> context)
        {
            try
            {
                var data = context.Message;

                _logger.LogInformation(message: context.Message.TradeId.ToString());

                var entity = new TradeNotification
                {
                    IsActive = true,
                    TradeId = data.TradeId,
                };

                _context.TradeNotifications.Add(entity);
                await _context.SaveChangesAsync(default);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: "Error while consuming TradeIdConsumer", ex.Message);
                throw;
            }
        }
    }
}
