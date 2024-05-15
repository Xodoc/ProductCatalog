using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.GetCategories
{
    internal sealed class Handler : IRequestHandler<Query, Result<IEnumerable<CategoryResponse>>>
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);

            if (categories is null)
                return Result.Failure<IEnumerable<CategoryResponse>>(Error.NullValue);

            var categoriesResponse = categories.Adapt<IEnumerable<CategoryResponse>>();

            return Result.Success(categoriesResponse);
        }
    }
}
