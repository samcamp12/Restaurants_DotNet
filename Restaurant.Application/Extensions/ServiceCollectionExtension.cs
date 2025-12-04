using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssemly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddScoped<IRestaurentsService, RestaurentsService>();

        services.AddAutoMapper(applicationAssemly);

        services.AddValidatorsFromAssembly(applicationAssemly)
            .AddFluentValidationAutoValidation();
    }
}