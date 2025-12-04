

using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants;

public interface IRestaurentsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> Create(CreateRestaurantDto dto);
}