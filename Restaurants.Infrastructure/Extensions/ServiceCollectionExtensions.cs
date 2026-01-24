using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantDbContext>(options => options
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
        );

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimPrincipleFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishRepository>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationlity, builder => builder.RequireClaim(AppClaimTypes.Nationlity, "Russian"));

    }
}


