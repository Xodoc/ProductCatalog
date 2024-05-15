using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Auth.Login
{
    public class Command : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
