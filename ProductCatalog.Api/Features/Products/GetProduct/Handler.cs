using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetProduct
{
    internal sealed class Handler : IRequestHandler<Query, Result<GetProductResponse>>
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetProductResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var productResponse = product.Adapt<GetProductResponse>();

            if (productResponse is null)
                return Result.Failure<GetProductResponse>(new Error("GetProduct.Null", "The product with the specified ID was not found"));

            return productResponse;
        }
    }
}
