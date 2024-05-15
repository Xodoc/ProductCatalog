using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.GetFilteredProducts
{
    internal sealed class Handler : IRequestHandler<Command, Result<IEnumerable<ProductResponse>>>
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure<IEnumerable<ProductResponse>>(new Error("GetFilteredProduct.Validation", validationResult.ToString()));

            var query = _context.Products.AsNoTracking()
                                         .Include(x => x.Category)
                                         .AsSplitQuery();

            if (string.IsNullOrWhiteSpace(request.Name) is false)
                query = query.Where(p => p.Name.Contains(request.Name));

            if (request.CategoryId is not 0)
                query = query.Where(p => p.CategoryId == request.CategoryId);

            if (string.IsNullOrWhiteSpace(request.Description) is false)
                query = query.Where(p => p.Description.Contains(request.Description));

            if (request.CostFrom > 0)
                query = query.Where(p => p.CostInRubles >= request.CostFrom);

            if (request.CostUpTo > 0)
                query = query.Where(p => p.CostInRubles <= request.CostUpTo);

            var products = await query.ToListAsync(cancellationToken);

            if (products is null)
                return Result.Failure<IEnumerable<ProductResponse>>(Error.NullValue);

            var response = products.Adapt<IEnumerable<ProductResponse>>();

            return Result.Success(response);
        }
    }
}
