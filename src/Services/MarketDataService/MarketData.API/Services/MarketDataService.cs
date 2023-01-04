using MarketData.API.Models;
using Newtonsoft.Json;

namespace MarketData.API.Services
{
    public class MarketDataService : IMarketDataService
    {
        private readonly ILogger<MarketDataService> _logger;

        public MarketDataService(ILogger<MarketDataService> logger)
        {
            _logger = logger;
        }
        public MarketDataModel GetMarketDataBySymbol(string symbol)
        {
            try
            {
                var data = GetAllMarketData().Where(x => x.Symbol.ToUpper() == symbol)
               .FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at GetMarketDataBySymbol,  error message : {ex.Message}", ex);
            }
            return null;
        }

        public IEnumerable<MarketDataModel> GetAllMarketData()
        {
            var data = new List<MarketDataModel>();
            try
            {

                using (StreamReader r = new StreamReader("MarketData.json"))
                {
                    string json = r.ReadToEnd();
                    MarketDataInput items = JsonConvert.DeserializeObject<MarketDataInput>(json);

                    foreach (var item in items.Data.Rates)
                    {
                        MarketDataModel marketData = new MarketDataModel
                        {
                            Price = item.Value,
                            Symbol = item.Key,
                        };
                        data.Add(marketData);
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error at GetMarketDataBySymbol,  error message : {ex.Message}", ex);
            }
            return data;
        }
    }
}
