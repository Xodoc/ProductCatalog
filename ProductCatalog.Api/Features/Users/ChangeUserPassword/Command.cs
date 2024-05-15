using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.ChangeUserPassword
{
    public class Command : IRequest<Result>
    {
        public string Id { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
