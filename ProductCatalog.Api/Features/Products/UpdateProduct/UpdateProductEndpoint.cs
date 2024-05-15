using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Api.Contracts;

namespace ProductCatalog.Api.Features.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/products/", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
            async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            }).WithTags("Products");
        }
    }
}
