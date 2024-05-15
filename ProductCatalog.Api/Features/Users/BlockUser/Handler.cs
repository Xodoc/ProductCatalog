using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.BlockUser
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
                return Result.Failure(new Error("BlockUser.Validation", validationResult.ToString()));

            var user = await _userManager.FindByIdAsync(request.Id);

            if (user is null)
                return Result.Failure(new Error("BlockUser.Null", "The user with the specified Id was not found"));

            if (user.LockoutEnabled is true)
                return Result.Failure(new Error("BlockUser.Blocked", "The user is already blocked"));

            var result = await _userManager.SetLockoutEnabledAsync(user, true);

            if (result.Succeeded is false)
                return Result.Failure(new Error("BlockUser.Error", result.ToString()));

            return Result.Success();
        }
    }
}
