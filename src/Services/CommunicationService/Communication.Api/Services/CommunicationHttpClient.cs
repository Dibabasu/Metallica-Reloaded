using Communications.Api.Exceptions;
using Newtonsoft.Json;

namespace Communications.Api.Services
{
    public class CommunicationHttpClient
    {
        private readonly HttpClient _httpClient;
        public CommunicationHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;   
        }

        public async Task<TResult> GetAsync<TResult>(string url, Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{url}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResult>(content);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new NotFoundException(typeof(TResult).Name.ToString(), id);
                    }
                    throw new Exception($"GetNotificationDetail failed for notificationId: {id}" +
                        $", statuscode {response.StatusCode} ");
                }
            }
            catch (Exception)
            {
                throw;
            }
           
        }
      
    }
}
