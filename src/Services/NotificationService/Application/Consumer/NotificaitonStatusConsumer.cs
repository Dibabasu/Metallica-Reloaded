using EventBus.RabbitMQ.Notifications.Communications;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notifications.Application.Notifications.Commands.UpdateNotification;
using Notifications.Domain.Common;

namespace Notifications.Application.Consumer
{
    public class NotificaitonStatusConsumer : IConsumer<NoticationStatusMessage>
    {

        private readonly ILogger<NotificaitonStatusConsumer> _logger;
        private readonly IMediator _mediator;
        public NotificaitonStatusConsumer(ILogger<NotificaitonStatusConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<NoticationStatusMessage> context)
        {
            try
            {
                var data = context.Message;
                _logger.LogInformation(message: context.Message.NotificaitonId.ToString());

                await _mediator.Send(new UpdateNotificationCommand
                {
                    EmailStatus = (NotificaitonStatus)data.EmailStatus,
                    SMSStatus = (NotificaitonStatus)data.SMSStatus,
                    Id = data.NotificaitonId,
                    NumberOfRetries = data.NumberOfRetries

                });

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
