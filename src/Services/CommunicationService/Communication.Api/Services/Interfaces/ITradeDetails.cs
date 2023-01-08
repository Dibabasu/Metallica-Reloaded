using Communications.Api.Model;

namespace Communications.Api.Services.Interfaces
{
    public interface ITradeDetails
    {
        Task<TradeDTO> GetTradeById(Guid TradeId);
    }
}
