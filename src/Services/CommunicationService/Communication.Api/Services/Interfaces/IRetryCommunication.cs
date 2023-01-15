using Communications.Api.Model;

namespace Communications.Api.Services.Interfaces
{
    public interface IRetryCommunication
    {
        public Task<EmailResponse> RetryEmail(Guid notificationId);
        public Task<SmsResponse> RetrySMS(Guid notificationId);
    }
}
