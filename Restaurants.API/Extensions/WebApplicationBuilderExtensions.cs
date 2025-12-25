using Microsoft.OpenApi;
using Restaurants.API.Middleware;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(doc =>
            {
                var requirement = new OpenApiSecurityRequirement();
                var schemeReference = new OpenApiSecuritySchemeReference("bearerAuth", doc);
                requirement.Add(schemeReference, new List<string>());
                return requirement;
            });
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        builder.Host.UseSerilog((context, config) => config
            .ReadFrom.Configuration(context.Configuration)
        );
    }
}
