using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Api.Contracts;

namespace ProductCatalog.Api.Features.Categories.CreateCategory
{
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/categories", [Authorize(Roles = "Advanced", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
            async (CreateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(new { Id = result.Value });
            }).WithTags("Categories");
        }
    }
}
