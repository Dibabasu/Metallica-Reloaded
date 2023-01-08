using Communications.Api.Model.Common;

namespace Communications.Api.Model
{
    public class EmailResponse
    {
        public NotificaitonStatus Status { get; set; }
        public int Retries { get; set; }

        public DateTime EmailSentAt { get; set; }

    }
}
