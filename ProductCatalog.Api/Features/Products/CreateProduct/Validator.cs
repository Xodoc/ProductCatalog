using FluentValidation;

namespace ProductCatalog.Api.Features.Products.CreateProduct
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.CategoryId).NotEqual(0);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.GeneralNote).NotEmpty();
            RuleFor(p => p.SpecialNote).NotEmpty();
            RuleFor(p => p.CostInRubles).NotEqual(0);
        }
    }
}
