﻿using Communications.Api.Model;

namespace Communications.Api.Services.Interfaces
{
    public interface ICommuncations
    {
        public Task<EmailResponse> SendEmail(NotificationDetail notificationDetail,TradeDTO trade);
        public Task<SmsResponse> SendSMS(NotificationDetail notificationDetail, TradeDTO trade);
    }
}
