using FluentValidation;
using MediatR;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Products.DeleteProduct
{
    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure(new Error("DeleteProduct.Validation", validationResult.ToString()));

            var product = await _context.Products.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (product is null)
                return Result.Failure(new Error("DeleteProduct.Null", "The product with the specified ID was not found"));

            _context.Products.Remove(product!);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
