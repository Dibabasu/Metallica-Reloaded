using Communications.Api.Exceptions;
using Communications.Api.Model;
using Communications.Api.Services.Interfaces;
using Newtonsoft.Json;

namespace Communications.Api.Services
{
    public class TradeDetailsService : ITradeDetails
    {
        private readonly ILogger<TradeDetailsService> _logger;
        private readonly IConfiguration _configuration;

        public TradeDetailsService(ILogger<TradeDetailsService> logger, IConfiguration configuration)
        {
            _logger=logger;
            _configuration = configuration;
        }
        public async Task<TradeDTO> GetTradeById(Guid TradeId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                 
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("TradeApiURI"));
                    var response = await client.GetAsync(string.Format("/{0}", TradeId));
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (result != null)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<TradeDTO>(jsonString);
                        }
                        
                    }
                    throw new NotFoundException(nameof(Services), TradeId);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"Error while Getting TradeById : {TradeId}");
                throw;
            }
            
        }
    }
}
