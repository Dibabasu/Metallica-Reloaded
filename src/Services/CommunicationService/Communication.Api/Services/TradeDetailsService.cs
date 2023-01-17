using Communications.Api.Model;
using Communications.Api.Model.Common;
using Communications.Api.Services.Interfaces;
using System.Net.Http;

namespace Communications.Api.Services
{
    public class TradeDetailsService : ITradeDetails
    {
        private readonly IConfiguration _configuration;
        private readonly CommunicationHttpClient _httpclient;

        public TradeDetailsService(ILogger<TradeDetailsService> logger,
            IConfiguration configuration,
            CommunicationHttpClient httpClientFactory)
        {
            _configuration = configuration;
            _httpclient = httpClientFactory;
        }
        public async Task<TradeDTO> GetTradeById(Guid TradeId)
        {
            try
            {
                return await _httpclient.GetAsync<TradeDTO>(
                     url: $"{_configuration[Utility.TradeApiURI]}{Utility.TradeById}",
                     id: TradeId
                     );
            }
            catch
            {
                throw;
            }

        }
    }
}
