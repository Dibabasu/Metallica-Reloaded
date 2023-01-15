using Communications.Api.Model;

namespace Communications.Api.Services.Interfaces
{
    public interface ICommuncations
    {
        public Task<EmailResponse> SendEmail(NotificationDetailDTO notificationDetail,TradeDTO trade);
        public Task<SmsResponse> SendSMS(NotificationDetailDTO notificationDetail, TradeDTO trade);
    }
}
