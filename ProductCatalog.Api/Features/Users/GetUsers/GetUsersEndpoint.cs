using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Features.Users.GetUsers
{
    public class GetUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/users", [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (ISender sender) =>
            {
                var query = new Query();

                var result = await sender.Send(query);

                return Results.Ok(result);
            }).WithTags("Users");
        }
    }
}
