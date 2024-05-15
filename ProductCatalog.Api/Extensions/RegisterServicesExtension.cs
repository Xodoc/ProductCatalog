using ProductCatalog.Api.Features.Auth.Services;

namespace ProductCatalog.Api.Extensions
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
