using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesByRestaurantQuery(int restaurant) : IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; } = restaurant;
}
