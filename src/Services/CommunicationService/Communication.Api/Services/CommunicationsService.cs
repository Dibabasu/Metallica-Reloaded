using Communications.Api.Exceptions;
using Communications.Api.Model;
using Communications.Api.Model.Common;
using Communications.Api.Services.Interfaces;
using Polly;
using Polly.Retry;

namespace Communications.Api.Services
{
    public class CommunicationsService : ICommuncations
    {
        private readonly ILogger _logger;
        private readonly AsyncRetryPolicy _retryPolicy;
        private static Random _random = new Random();
        private const int MaxRetries = 3;
       
        public CommunicationsService(ILogger<CommunicationsService> logger
            )
        {
            _logger = logger;
            _retryPolicy = Policy.Handle<Exception>().RetryAsync(MaxRetries);
        }

        public async Task<EmailResponse> SendEmail(NotificationDetailDTO notificationDetail, TradeDTO trade)
        {
            int retrycount = 0;
            try
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    retrycount += 1;
                    Random random = new Random();
                    if (random.Next(1, 10) > 2)
                    {

                        throw new EmailException();
                    }

                    _logger.LogInformation($"A New Email has been sent, Notification Id : {notificationDetail.NotificationId} " +
                        $"and Trade Id : {notificationDetail.TradeId} " +
                        $"with TradeDetails : {GetStringTradeDetail(trade)} ");

                    return new EmailResponse
                    {
                        EmailSentAt = DateTime.UtcNow,
                        Retries = retrycount,
                        Status = NotificaitonStatus.Sent
                    };

                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Send EMail, Notification Id : {notificationDetail.NotificationId} and Trade Id : {notificationDetail.TradeId} ");
                _logger.LogError($"Error : {ex.Message}");

                return new EmailResponse
                {
                    Retries = retrycount,
                    Status = NotificaitonStatus.Failed
                };
            }
        }

        public async Task<SmsResponse> SendSMS(NotificationDetailDTO notificationDetail, TradeDTO trade)
        {
            try
            {
                Random random = new Random();

                if (random.Next(1, 5) == 5)
                    throw new SMSException();

                _logger.LogInformation($"A New SMS has been sent, Notification Id : {notificationDetail.NotificationId} " +
                    $"and Trade Id : {notificationDetail.TradeId} " +
                    $"with TradeDetails : {GetStringTradeDetail(trade)} ");

                return new SmsResponse
                {
                    Status = NotificaitonStatus.Sent,
                    SmsSentAt = DateTime.UtcNow
                };
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to Send SMS, Notification Id : {notificationDetail.NotificationId} and Trade Id : {notificationDetail.TradeId} ");
                return new SmsResponse
                {
                    Status = NotificaitonStatus.Failed,
                };
            }
        }

        private static string GetStringTradeDetail(TradeDTO tradeDetails)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(tradeDetails);
        }
       
    }
}
