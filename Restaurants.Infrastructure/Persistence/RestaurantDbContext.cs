using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantDbContext : DbContext
{
    internal DbSet<Restaurant> Restaurants { get; set; }
}







