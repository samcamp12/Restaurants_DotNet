using Restaurants.Application.Common;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection);
    Task<Restaurant?> GetOneByIdAsync(int id);
    Task<int> CreateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);

    Task SaveChanges();
    Task UpdateAsync(Restaurant restaurant);
}

