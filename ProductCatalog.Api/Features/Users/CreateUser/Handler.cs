using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Users.CreateUser
{
    internal sealed class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly UserManager<User> _userManager;

        public Handler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
                return Result.Failure<string>(new Error("CreateUser.Null", "The user with the specified email already exists"));

            var newUser = new User { UserName = request.UserName, Email = request.Email };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded is false)
                return Result.Failure<string>(new Error("CreateUser.Error", result.ToString()));

            return Result.Success(newUser.Id);
        }
    }
}
