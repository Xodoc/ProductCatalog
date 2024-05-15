using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.BlockUser
{
    public class Command : IRequest<Result>
    {
        public string Id { get; set; } = string.Empty;
    }
}
