

using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Restaurants;

public interface IRestaurentsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto?> GetRestaurantById(int id);
    Task<int> Create(CreateRestaurantDto dto);
}