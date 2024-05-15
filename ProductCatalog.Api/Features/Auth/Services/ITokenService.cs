using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Auth.Services
{
    public interface ITokenService
    {
        Task<Result<string>> GenerateTokenAsync(User user);
    }
}
