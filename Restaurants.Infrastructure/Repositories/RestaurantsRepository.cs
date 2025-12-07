using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantsRepository(RestaurantDbContext dbContext): IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetOneByIdAsync(int restaurantId)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .SingleOrDefaultAsync(r => r.Id == restaurantId);
        return restaurant;
    }

    public async Task<int> CreateAsync (Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        dbContext.Restaurants.Update(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
    
}

