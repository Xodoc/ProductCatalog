using Carter;
using Mapster;
using MediatR;
using ProductCatalog.Api.Contracts;

namespace ProductCatalog.Api.Features.Products.GetFilteredProducts
{
    public class GetFilteredProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/products/filter", async (ProductFilterRequest request, ISender sender) =>
            {
                var command = request.Adapt<Command>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            }).WithTags("Products").RequireAuthorization();
        }
    }
}
