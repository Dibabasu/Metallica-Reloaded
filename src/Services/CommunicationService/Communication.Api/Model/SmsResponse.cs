using Communications.Api.Model.Common;

namespace Communications.Api.Model
{
    public class SmsResponse
    {
        public NotificaitonStatus Status { get; set; }
        public DateTime SmsSentAt { get; set; }
    }
}
