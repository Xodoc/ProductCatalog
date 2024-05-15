using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetFilteredProducts
{
    public class Command : IRequest<Result<IEnumerable<ProductResponse>>>
    {
        public string Name { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public double CostFrom { get; set; }

        public double CostUpTo { get; set; }
    }
}
