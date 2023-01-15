using Communications.Api.Consumers;
using Communications.Api.Model;
using Communications.Api.Publisher.Interfaces;
using Communications.Api.Services.Interfaces;
using EventBus.RabbitMQ.Notifications.Communications;
using Newtonsoft.Json;
using System.Net.Http;

namespace Communications.Api.Services
{
    public class RetryCommunicationService : IRetryCommunication
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly ICommuncations _communcationsService;
        private IPublisherService _publisherService;
        private ITradeDetails _tradeDetails;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public RetryCommunicationService(ILogger<NotificationConsumer> logger,
           ICommuncations iCommuncations,
           IPublisherService publisherService,
           ITradeDetails tradeDetails,
           IConfiguration configuration,
           IHttpClientFactory httpClientFactory
           )
        {
            _logger = logger;
            _communcationsService = iCommuncations;
            _publisherService = publisherService;
            _tradeDetails = tradeDetails;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<EmailResponse> RetryEmail(Guid notificationId)
        {
            try
            {
                var notification = await GetNotificationDetail(notificationId);
                var tradeDetails = await _tradeDetails.GetTradeById(notification.TradeId);
                var emailResponse = await _communcationsService.SendEmail(notification, tradeDetails);

                var noticationStatusMessage = new NoticationStatusMessage
                {
                    NotificaitonId = notificationId,
                    EmailStatus = (int)emailResponse.Status,
                    NumberOfRetries = emailResponse.Retries
                };

                await _publisherService.UpdateNotificaitonStatus(noticationStatusMessage);

                return emailResponse;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while RetryEmail for notificationId: {notificationId}" +
                    $", exception {ex.Message}");
                throw;

            }
        }
        public async Task<SmsResponse> RetrySMS(Guid notificationId)
        {
            try
            {
                var notification = await GetNotificationDetail(notificationId);
                var tradeDetails = await _tradeDetails.GetTradeById(notification.TradeId);
                var smsResponse = await _communcationsService.SendSMS(notification, tradeDetails);

                var noticationStatusMessage = new NoticationStatusMessage
                {
                    NotificaitonId = notificationId,
                    EmailStatus = (int)smsResponse.Status,
                    NumberOfRetries = -1
                };

                await _publisherService.UpdateNotificaitonStatus(noticationStatusMessage);

                return smsResponse;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while RetryEmail for notificationId: {notificationId}" +
                    $", exception {ex.Message}");
                throw;

            }
        }

        private async Task<NotificationDetailDTO> GetNotificationDetail(Guid notificationId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_configuration["NotificationServiceUrl"]}/api/notification/{notificationId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NotificationDetailDTO>(content);
            }
            else
            {
                throw new Exception($"Error while getting notification details for notificationId: {notificationId}" +
                    $", statuscode {response.StatusCode}");
            }
        }
    }
}
