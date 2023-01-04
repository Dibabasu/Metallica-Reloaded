using Newtonsoft.Json;

namespace MarketData.API.Models
{
    public class MarketDataModel

    {
        public string Symbol { get; set; } = string.Empty;
        public double Price { get; set; }

    }
    public class MarketDataInput
    {
        [JsonProperty("data")]
        public MarketDataInputBody Data { get; set; }
    }
    public class MarketDataInputBody
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; } = string.Empty;

        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}
