using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.UpdateCategory
{
    public class Command : IRequest<Result<CategoryResponse>>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
