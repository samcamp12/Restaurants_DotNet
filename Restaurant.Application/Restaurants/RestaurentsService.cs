using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurentsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurentsService> logger) : IRestaurentsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Get all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int restaurantId)
    {
        logger.LogInformation($"Get this restaurant with id: {restaurantId}");
        var restaurant = await restaurantsRepository.GetOneByIdAsync(restaurantId);
        var restaurantsDto = RestaurantDto.FromEntity(restaurant);
        return restaurantsDto;
    }
}

