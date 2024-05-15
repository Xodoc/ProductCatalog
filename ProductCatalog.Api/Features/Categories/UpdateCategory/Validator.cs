using FluentValidation;

namespace ProductCatalog.Api.Features.Categories.UpdateCategory
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(p => p.Id).NotEqual(0);
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
