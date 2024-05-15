using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.HttpDataClients.Interfaces
{
    public interface IHttpCategoryDataClient
    {
        Task<List<CategoryModel>> GetCategoriesAsync(string jwt);

        Task<int> CreateCategoryAsync(string name, string jwt);

        Task<bool> UpdateCategoryAsync(UpdateCategoryRequest request, string jwt);

        Task<bool> DeleteCategoryByIdAsync(int id, string jwt);
    }
}
