using ProductCatalog.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using ProductCatalog.Client.HttpDataClients.Interfaces;

namespace ProductCatalog.Client.HttpDataClients
{
    public sealed class HttpAuthDataClient : IHttpAuthDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpAuthDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> LoginAsync(AuthRequest request)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PostAsync($"{_config["BaseAddress"]}/auth/login", httpContent);

            if (responseMessage.IsSuccessStatusCode)
                return await responseMessage.Content.ReadFromJsonAsync<string>();

            return string.Empty;
        }
    }
}
