
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;


namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
                                new Restaurant
                {
                    Name = "Italian Bistro",
                    Description = "Cozy place for authentic Italian cuisine.",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "info@italianbistro.com",
                },
                new Restaurant
                {
                    Name = "Sushi World",
                    Description = "Fresh sushi and sashimi made to order.",
                    Category = "Japanese",
                    HasDelivery = false,
                    ContactEmail = "info@sushiworld.com"
                }
            ];
            return restaurants;
        }
    }
}
