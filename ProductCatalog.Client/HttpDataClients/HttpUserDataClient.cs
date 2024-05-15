using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ProductCatalog.Client.HttpDataClients
{
    public sealed class HttpUserDataClient : IHttpUserDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpUserDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<bool> BlockUserByIdAsync(string id, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PutAsync($"{_config["BaseAddress"]}/users/{id}/block", content);

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPasswordAsync(string id, string newPassword, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var model = new { Id = id, NewPassword = newPassword };

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PutAsync($"{_config["BaseAddress"]}/users/password/change", content);

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<string> CreateUserAsync(CreateUserRequest request, string jwt)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

                var responseMessage = await _httpClient.PostAsync($"{_config["BaseAddress"]}/users", content);

                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadAsStringAsync();

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<bool> DeleteUserByIdAsync(string id, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var responseMessage = await _httpClient.DeleteAsync($"{_config["BaseAddress"]}/users/{id}");

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<UserModel>> GetUsersAsync(string jwt)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var responseMessage = await _httpClient.GetAsync($"{_config["BaseAddress"]}/users");

                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadFromJsonAsync<List<UserModel>>();

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
