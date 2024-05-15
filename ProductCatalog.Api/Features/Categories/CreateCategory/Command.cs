using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.CreateCategory
{
    public class Command : IRequest<Result<int>>
    {
        public string Name { get; set; } = string.Empty;
    }
}
