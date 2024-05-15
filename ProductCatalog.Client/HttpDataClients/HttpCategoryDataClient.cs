﻿using ProductCatalog.Client.Models;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using ProductCatalog.Client.HttpDataClients.Interfaces;

namespace ProductCatalog.Client.HttpDataClients
{
    public sealed class HttpCategoryDataClient : IHttpCategoryDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCategoryDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<int> CreateCategoryAsync(string name, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var content = new StringContent(JsonSerializer.Serialize(new { Name = name }), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PostAsync($"{_config["BaseAddress"]}/categories", content);

            if (responseMessage.IsSuccessStatusCode)
                return 1;

            return -1;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var responseMessage = await _httpClient.DeleteAsync($"{_config["BaseAddress"]}/categories/{id}");

            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync(string jwt)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                var responseMessage = await _httpClient.GetAsync($"{_config["BaseAddress"]}/categories");

                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadFromJsonAsync<List<CategoryModel>>();

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryRequest request, string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, Application.Json);

            var responseMessage = await _httpClient.PutAsync($"{_config["BaseAddress"]}/categories/", content);

            return responseMessage.IsSuccessStatusCode;
        }
    }
}
