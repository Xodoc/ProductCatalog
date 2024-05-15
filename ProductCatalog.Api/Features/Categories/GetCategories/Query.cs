using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.GetCategories
{
    public class Query : IRequest<Result<IEnumerable<CategoryResponse>>> { }
}
