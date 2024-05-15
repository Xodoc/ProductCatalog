using FluentValidation;

namespace ProductCatalog.Api.Features.Users.BlockUser
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
