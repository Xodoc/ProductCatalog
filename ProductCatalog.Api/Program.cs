using Carter;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductCatalog.Api.Database;
using ProductCatalog.Api.Entities;
using ProductCatalog.Api.Extensions;
using ProductCatalog.Api.Middleware;
using Serilog;
using System.Reflection;

namespace ProductCatalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.Services.AddIdentity<User, IdentityRole>(opt => 
            { 
                opt.Lockout.AllowedForNewUsers = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            })
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var assembly = Assembly.GetExecutingAssembly();

            TypeAdapterConfig.GlobalSettings.Scan(assembly);

            builder.Configuration.AddUserSecrets(assembly);

            builder.Services.AddServices();
            builder.Services.AddValidatorsFromAssembly(assembly);
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            builder.Services.AddCarter();
            builder.Services.AddLogging(x => x.AddSerilog());

            builder.Services.AddCors();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.ApplayMigrations();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapCarter();

            app.Run();
        }
    }
}
