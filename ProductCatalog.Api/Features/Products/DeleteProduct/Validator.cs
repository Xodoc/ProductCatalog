using FluentValidation;

namespace ProductCatalog.Api.Features.Products.DeleteProduct
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEqual(0);
        }
    }
}
