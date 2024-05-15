using FluentValidation;
using MediatR;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.DeleteCategory
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
                return Result.Failure(new Error("DeleteCategory.Validation", validationResult.ToString()));

            var category = await _context.Categories.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (category is null)
                return Result.Failure(new Error("DeleteCategory.Null", "The category with the specified ID was not found"));

            _context.Categories.Remove(category!);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
