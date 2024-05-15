using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Database;

namespace ProductCatalog.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplayMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();
        }
    }
}
