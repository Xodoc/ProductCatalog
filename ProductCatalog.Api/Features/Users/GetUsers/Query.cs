using MediatR;
using ProductCatalog.Api.Contracts;

namespace ProductCatalog.Api.Features.Users.GetUsers
{
    public class Query : IRequest<IEnumerable<UserResponse>>
    {
    }
}
