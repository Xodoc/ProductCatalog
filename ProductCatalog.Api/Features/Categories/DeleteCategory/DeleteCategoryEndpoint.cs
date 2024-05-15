using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Features.Categories.DeleteCategory
{
    public class DeleteCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/categories/{id}", [Authorize(Roles = "Advanced", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
            async (int id, ISender sender) =>
            {
                var command = new Command { Id = id };

                var result = await sender.Send(command);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok();
            }).WithTags("Categories");
        }
    }
}
