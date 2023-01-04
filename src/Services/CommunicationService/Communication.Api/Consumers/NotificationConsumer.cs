using Communications.Api.Model;
using Communications.Api.Publisher.Interfaces;
using Communications.Api.Services.Interfaces;
using EventBus.RabbitMQ.Notifications.Communications;
using MassTransit;

namespace Communications.Api.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationMessage>
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly ICommuncations _communcationsService;
        private readonly IPublisherService _publisherService;
        public NotificationConsumer(ILogger<NotificationConsumer> logger, ICommuncations iCommuncations, IPublisherService publisherService)
        {
            _logger = logger;
            _communcationsService = iCommuncations;
            _publisherService = publisherService;
        }
        public async Task Consume(ConsumeContext<NotificationMessage> context)
        {
            try
            {
                var data = context.Message;
                _logger.LogInformation(message: context.Message.NotificationId.ToString());

                NotificationDetail notification = new()
                {
                    NotificationId = data.NotificationId,
                    TradeId = data.TradeId
                };

                var noticationStatusMessage = await SendNotifications(notification);

                await PublishNotificaitonStatus(noticationStatusMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<NoticationStatusMessage> SendNotifications(NotificationDetail notificationDetail)
        {
            var response = new NoticationStatusMessage();
            response.NotificaitonId = notificationDetail.NotificationId;
            try
            {
                var EmailStaus = await _communcationsService.SendEmail(notificationDetail);

                response.EmailStatus = (int)EmailStaus.Status;
                response.NumberOfRetries = EmailStaus.Retries;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't send email for notificaiton Id: {notificationDetail.NotificationId}, error - {ex.Message}");
            }

            try
            {
                var SMSStaus = await _communcationsService.SendSMS(notificationDetail);
                response.SMSStatus = (int)SMSStaus.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't send email for notificaiton Id: {notificationDetail.NotificationId}, error - {ex.Message}");
            }
            return response;

        }
        private async Task PublishNotificaitonStatus(NoticationStatusMessage noticationStatusMessage)
        {
            await _publisherService.UpdateNotificaitonStatus(noticationStatusMessage);
        }
    }
}