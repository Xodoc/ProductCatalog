using FluentValidation;
using Mapster;
using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.UpdateProduct
{
    internal sealed class Handler : IRequestHandler<Command, Result<ProductResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<ProductResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure<ProductResponse>(new Error("UpdateProduct.Validation", validationResult.ToString()));

            var product = await _context.Products.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (product is null)
                return Result.Failure<ProductResponse>(new Error("UpdateProduct.Null", "The product with the specified ID was not found"));

            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Description = request.Description;
            product.CostInRubles = request.CostInRubles;
            product.GeneralNote = request.GeneralNote;
            product.SpecialNote = request.SpecialNote;

            _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);

            var responseProduct = product.Adapt<ProductResponse>();

            return Result.Success(responseProduct);
        }
    }
}
