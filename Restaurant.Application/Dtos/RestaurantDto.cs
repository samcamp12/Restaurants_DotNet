using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public List<DishDto>? Dishes { get; set; } = new();

    public static RestaurantDto? FromEntity(Restaurant? restaurant)
    {
        if (restaurant is null) return null;
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            Category = restaurant.Category,
            HasDelivery = restaurant.HasDelivery,
            Street = restaurant.Address?.Street ?? string.Empty,
            City = restaurant.Address?.City ?? string.Empty,
            PostalCode = restaurant.Address?.PostalCode ?? string.Empty,
        };
    }
}

