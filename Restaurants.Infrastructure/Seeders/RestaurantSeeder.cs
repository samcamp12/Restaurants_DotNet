using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
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

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                    new (UserRoles.User)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new (UserRoles.Admin)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                    new (UserRoles.Owner)
                    {
                        NormalizedName = UserRoles.User.ToUpper()
                    },
                ];
            return roles;
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
                    Dishes = [
                        new Dish
                        {
                            Name = "Margherita Pizza",
                            Description = "Classic pizza with tomato sauce, mozzarella, and fresh basil",
                            Price = 12.99M,
                            KiloCalorities = 850
                        },
                        new Dish
                        {
                            Name = "Spaghetti Carbonara",
                            Description = "Pasta with eggs, pecorino cheese, guanciale, and black pepper",
                            Price = 14.50M,
                            KiloCalorities = 920
                        },
                        new Dish
                        {
                            Name = "Tiramisu",
                            Description = "Classic Italian dessert with coffee-soaked ladyfingers and mascarpone",
                            Price = 6.99M,
                            KiloCalorities = 450
                        }
                    ]
                },
                new Restaurant
                {
                    Name = "Sushi World",
                    Description = "Fresh sushi and sashimi made to order.",
                    Category = "Japanese",
                    HasDelivery = false,
                    ContactEmail = "info@sushiworld.com",
                    Dishes = [
                        new Dish
                        {
                            Name = "California Roll",
                            Description = "Crab, avocado, and cucumber wrapped in rice and nori",
                            Price = 8.99M,
                            KiloCalorities = 320
                        },
                        new Dish
                        {
                            Name = "Salmon Nigiri",
                            Description = "Fresh salmon over pressed vinegared rice",
                            Price = 5.99M,
                            KiloCalorities = 180
                        },
                        new Dish
                        {
                            Name = "Miso Soup",
                            Description = "Traditional Japanese soup with tofu and seaweed",
                            Price = 3.50M,
                            KiloCalorities = 80
                        }
                    ]
                }
            ];
            return restaurants;
        }
    }
}
