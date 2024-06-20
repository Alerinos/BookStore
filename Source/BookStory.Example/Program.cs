using BookStore.Application;
using BookStory.Infrastructure;
using BookStory.Presentation;
using Microsoft.OpenApi.Models;

namespace BookStory.Example;

/*
 * dotnet user-jwts create --scope "bookStore" --role "developer"
 */

internal class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddConfiguration(CreateConfiguration(args));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("jwt_auth", new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            });

            var securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {securityScheme, []},
            });
        });

        builder.Services
            .AddAuthorization()
            .AddAuthentication("Bearer")
            .AddJwtBearer();

        builder.Services.AddCors();

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("read", policy =>
                policy.RequireRole("developer").RequireClaim("scope", "bookStore")
            );

        builder.Services
            .AddBookStoryApplication()
            .AddBookStoryInfrastructure();

        var app = builder.Build();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseApi();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
                context.Response.Redirect("/swagger/index.html");
            else
                await next();
        });

        app.Run();
    }

    public static IConfiguration CreateConfiguration(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddCommandLine(args);

        var configuration = builder.Build();

        if (!File.Exists("appsettings.Development.json"))
            throw new FileNotFoundException("Settings file appsettings.Development.json not found");

        return configuration;
    }
}
