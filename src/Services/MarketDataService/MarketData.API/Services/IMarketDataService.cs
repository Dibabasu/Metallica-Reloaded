using MarketData.API.Models;

namespace MarketData.API.Services
{
    public interface IMarketDataService
    {
        MarketDataModel GetMarketDataBySymbol(string symbol);
        IEnumerable<MarketDataModel> GetAllMarketData();


    }
}
