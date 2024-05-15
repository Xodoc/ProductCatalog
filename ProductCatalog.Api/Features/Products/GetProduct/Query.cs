using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetProduct
{
    public class Query : IRequest<Result<GetProductResponse>>
    {
        public int Id { get; set; }
    }
}
