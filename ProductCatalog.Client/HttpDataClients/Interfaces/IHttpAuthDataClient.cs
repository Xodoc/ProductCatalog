using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.HttpDataClients.Interfaces
{
    public interface IHttpAuthDataClient
    {
        Task<string> LoginAsync(AuthRequest request);
    }
}
