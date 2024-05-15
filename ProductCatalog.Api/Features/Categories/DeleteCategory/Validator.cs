using FluentValidation;

namespace ProductCatalog.Api.Features.Categories.DeleteCategory
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(0);
        }
    }
}
