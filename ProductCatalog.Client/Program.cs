using ProductCatalog.Client.HttpDataClients;
using ProductCatalog.Client.HttpDataClients.Interfaces;

namespace ProductCatalog.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient<IHttpAuthDataClient, HttpAuthDataClient>();
            builder.Services.AddHttpClient<IHttpUserDataClient, HttpUserDataClient>();
            builder.Services.AddHttpClient<IHttpCategoryDataClient, HttpCategoryDataClient>();
            builder.Services.AddHttpClient<IHttpProductDataClient, HttpProductDataClient>();
            builder.Services.AddHttpClient<IHttpNbrbDataClient, HttpNbrbDataClient>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication()
                            .AddCookie();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
