using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetProducts
{
    internal sealed class Handler : IRequestHandler<Query, Result<IEnumerable<ProductResponse>>>
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.AsNoTracking()
                                                  .Include(x => x.Category)
                                                  .ToListAsync(cancellationToken);

            var productsResponse = products.Adapt<IEnumerable<ProductResponse>>();

            if (productsResponse is null)
                return Result.Failure<IEnumerable<ProductResponse>>(Error.NullValue);

            return Result.Success(productsResponse);
        }
    }
}
