using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ProductCatalog.Api.Features.Categories.GetCategories
{
    public class GetCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/categories", [Authorize(Roles = "Advanced", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (ISender sender) =>
            {
                var query = new Query();

                var result = await sender.Send(query);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            }).WithTags("Categories");
        }
    }
}
