using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.HttpDataClients.Interfaces
{
    public interface IHttpProductDataClient
    {
        Task<List<ProductModel>> GetProductsAsync(string jwt);

        Task<UpdateProductRequest> GetProductByIdAsync(int id, string jwt);

        Task<int> CreateProductAsync(CreateProductRequest request, string jwt);

        Task<bool> UpdateProductAsync(UpdateProductRequest request, string jwt);

        Task<bool> DeleteProductByIdAsync(int id, string jwt);
    }
}
