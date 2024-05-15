using FluentValidation;

namespace ProductCatalog.Api.Features.Users.DeleteUser
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
