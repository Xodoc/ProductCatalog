using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.CreateUser
{
    public class Command : IRequest<Result<string>>
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
