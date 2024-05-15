using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.HttpDataClients
{
    public class HttpNbrbDataClient : IHttpNbrbDataClient
    {
        private readonly HttpClient _httpClient;

        public HttpNbrbDataClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync()
        {
            var curId = 431;
            var rate = await GetRateAsync(curId);

            return (decimal)rate.Cur_OfficialRate;
        }

        private async Task<Rate> GetRateAsync(int curId)
        {
            var responseMessage = await _httpClient.GetAsync($"https://api.nbrb.by/exrates/rates/{curId}");

            if (!responseMessage.IsSuccessStatusCode)
                return null;

            var result = await responseMessage.Content.ReadFromJsonAsync<Rate>();

            return result;
        }
    }
}
