using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurentsService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    }
}