using ProductCatalog.Client.Models;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using ProductCatalog.Client.HttpDataClients.Interfaces;

namespace ProductCatalog.Client.HttpDataClients
{
    public sealed class HttpProductDataClient : IHttpProductDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpProductDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<int> CreateProductAsync(CreateProductRequest request, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PostAsync($"{_config["BaseAddress"]}/products", content);

            if (responseMessage.IsSuccessStatusCode)
                return 1;

            return -1;
        }

        public async Task<bool> DeleteProductByIdAsync(int id, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var responseMessage = await _httpClient.DeleteAsync($"{_config["BaseAddress"]}/products/{id}");

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<UpdateProductRequest> GetProductByIdAsync(int id, string jwt)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var responseMessage = await _httpClient.GetAsync($"{_config["BaseAddress"]}/products/{id}");

                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadFromJsonAsync<UpdateProductRequest>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProductModel>> GetProductsAsync(string jwt)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var responseMessage = await _httpClient.GetAsync($"{_config["BaseAddress"]}/products");

                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadFromJsonAsync<List<ProductModel>>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateProductAsync(UpdateProductRequest request, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PutAsync($"{_config["BaseAddress"]}/products/", content);

            return responseMessage.IsSuccessStatusCode;
        }
    }
}
