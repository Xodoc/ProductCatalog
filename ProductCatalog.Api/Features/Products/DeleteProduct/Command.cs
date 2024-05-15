using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.DeleteProduct
{
    public class Command : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
