using FluentValidation;
using Mapster;
using MediatR;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.CreateCategory
{
    internal sealed class Handler : IRequestHandler<Command, Result<int>>
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(AppDbContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure<int>(new Error("CreateCategory.Validation", validationResult.ToString()));

            var category = request.Adapt<Category>();

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
