using FluentValidation;
using Mapster;
using MediatR;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.UpdateCategory
{
    internal sealed class Handler : IRequestHandler<Command, Result<CategoryResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<CategoryResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure<CategoryResponse>(new Error("UpdateCategory.Validation", validationResult.ToString()));

            var category = await _context.Categories.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (category is null)
                return Result.Failure<CategoryResponse>(new Error("UpdateCategory.Null", "The category with the specified ID was not found"));

            category.Name = request.Name;

            _context.Categories.Update(category);

            await _context.SaveChangesAsync(cancellationToken);

            var responseCategory = category.Adapt<CategoryResponse>();

            return Result.Success(responseCategory);
        }
    }
}
