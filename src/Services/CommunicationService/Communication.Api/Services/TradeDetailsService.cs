using Communications.Api.Exceptions;
using Communications.Api.Model;
using Communications.Api.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;

namespace Communications.Api.Services
{
    public class TradeDetailsService : ITradeDetails
    {
        private readonly ILogger<TradeDetailsService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;


        public TradeDetailsService(ILogger<TradeDetailsService> logger,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<TradeDTO> GetTradeById(Guid TradeId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_configuration["TradeApiURI"]}/api/trade/{TradeId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TradeDTO>(content);
            }
            else
            {
                throw new Exception($"Error in GetTradeById for TradeId: {TradeId}" +
                    $", statuscode {response.StatusCode}");
            }
        }
    }
}
