using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Features.Auth.Services;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Auth.Login
{
    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public Handler(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var validationResult = await ValidateUserAsync(user!, request.Password);

            if (validationResult.IsFailure)
                return Result.Failure<string>(validationResult.Error);

            return await _tokenService.GenerateTokenAsync(user!);
        }

        private async Task<Result> ValidateUserAsync(User user, string password)
        {
            if (user is null)
                return Result.Failure(new Error("Login.Null", "The user with the specified email was not found"));

            if (!await _userManager.CheckPasswordAsync(user!, password))
                return Result.Failure(new Error("Login.IncorrectPassword", "The password was not correct"));

            if (user.LockoutEnabled is true)
                return Result.Failure(new Error("Login.Blocked", "The user with the specified email was blocked"));

            return Result.Success();
        }
    }
}
