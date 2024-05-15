using Mapster;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Entities;

namespace ProductCatalog.Api.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponse>()
                  .Map(dest => dest.CategoryName, src => src.Category!.Name);
        }
    }
}
