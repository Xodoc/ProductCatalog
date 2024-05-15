using Carter;
using Mapster;
using MediatR;
using ProductCatalog.Api.Contracts;

namespace ProductCatalog.Api.Features.Auth.Login
{
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/login", async (AuthRequest request, ISender sender) =>
            {
                var command = request.Adapt<Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            }).WithTags("Auth");
        }
    }
}
