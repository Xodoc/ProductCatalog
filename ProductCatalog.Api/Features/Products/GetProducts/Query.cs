using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetProducts
{
    public class Query : IRequest<Result<IEnumerable<ProductResponse>>> { }
}
