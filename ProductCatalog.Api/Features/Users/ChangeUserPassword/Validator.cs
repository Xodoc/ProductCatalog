using FluentValidation;

namespace ProductCatalog.Api.Features.Users.ChangeUserPassword
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
        }
    }
}
