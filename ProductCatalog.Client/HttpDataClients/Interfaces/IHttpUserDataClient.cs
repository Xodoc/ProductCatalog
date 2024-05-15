using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.HttpDataClients.Interfaces
{
    public interface IHttpUserDataClient
    {
        Task<List<UserModel>> GetUsersAsync(string jwt);

        Task<bool> DeleteUserByIdAsync(string id, string jwt);

        Task<string> CreateUserAsync(CreateUserRequest request, string jwt);

        Task<bool> ChangeUserPasswordAsync(string id, string newPassword, string jwt);

        Task<bool> BlockUserByIdAsync(string id, string jwt);
    }
}
