using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using System.Collections.Generic;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{
}
