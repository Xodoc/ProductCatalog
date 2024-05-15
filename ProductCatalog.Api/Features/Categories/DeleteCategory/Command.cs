using MediatR;
using ProductCatalog.Api.Shared;

namespace ProductCatalog.Api.Features.Categories.DeleteCategory
{
    public class Command : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
