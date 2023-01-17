using Communications.Api.Consumers;
using Communications.Api.Exceptions;
using Communications.Api.Model;
using Communications.Api.Model.Common;
using Communications.Api.Publisher.Interfaces;
using Communications.Api.Services.Interfaces;
using EventBus.RabbitMQ.Notifications.Communications;
using MassTransit;
using Microsoft.Extensions.Logging;
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
        private readonly CommunicationHttpClient _httpclient;
        private readonly IConfiguration _configuration;

        
        public RetryCommunicationService(ILogger<NotificationConsumer> logger,
           ICommuncations iCommuncations,
           IPublisherService publisherService,
           ITradeDetails tradeDetails,
           IConfiguration configuration,
           CommunicationHttpClient httpClientFactory
           )
        {
            _logger = logger;
            _communcationsService = iCommuncations;
            _publisherService = publisherService;
            _tradeDetails = tradeDetails;
            _httpclient = httpClientFactory;
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
                _logger.LogError($"Retry email failed for notificationId : {notificationId}" +
                     $", exception {ex.Message}");
                throw new Exception($"Retry email failed for notificationId : {notificationId}");

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
                _logger.LogError($"Retry SMS failed for notificationId : {notificationId}" +
                    $", exception {ex.Message}");
                throw new Exception($"Retry SMS failed for notificationId : {notificationId}");
            }
        }

        private async Task<NotificationDetailDTO> GetNotificationDetail(Guid notificationId)
        {
            return await _httpclient.GetAsync<NotificationDetailDTO>(
                   url: $"{_configuration[Utility.NotificationServiceUrl]}{Utility.NotificationById}",
                   id: notificationId
                   );
        }
    }
}
