using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.DeleteUser
{
    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly IValidator<Command> _validator;

        public Handler(UserManager<User> userManager, IValidator<Command> validator)
        {
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (validationResult.IsValid is false)
                return Result.Failure(new Error("DeleteUser.Validation", validationResult.ToString()));

            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
                return Result.Failure(new Error("DeleteUser.Null", "The user with the specified Id was not found"));

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? Result.Success() : Result.Failure(new Error("DeleteUser.Error", result.ToString()));
        }
    }
}
