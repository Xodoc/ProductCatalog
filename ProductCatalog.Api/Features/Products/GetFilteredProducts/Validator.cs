using FluentValidation;

namespace ProductCatalog.Api.Features.Products.GetFilteredProducts
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(p => p.CostFrom).Must(x => x >= 0 && x <= double.MaxValue);
            RuleFor(p => p.CostUpTo).Must(x => x >= 0 && x <= double.MaxValue);
        }
    }
}
