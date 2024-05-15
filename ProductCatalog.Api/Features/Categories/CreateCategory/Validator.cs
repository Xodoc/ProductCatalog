using FluentValidation;

namespace ProductCatalog.Api.Features.Categories.CreateCategory
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
