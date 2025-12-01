using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurentsService(IRestaurantsRepository restaurantsRepository, 
    ILogger<RestaurentsService> logger,
    IMapper mapper
    ) : IRestaurentsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Get all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsDto!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int restaurantId)
    {
        logger.LogInformation($"Get this restaurant with id: {restaurantId}");
        var restaurant = await restaurantsRepository.GetOneByIdAsync(restaurantId);
        var restaurantsDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantsDto;
    }

    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Create a new restaurant");
        var restaurant = mapper.Map<Restaurant>(dto);
        int id = await restaurantsRepository.CreateAsync(restaurant);
        return id;
    }
}

