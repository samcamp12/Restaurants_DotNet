using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesByRestaurant;

public class GetDishesByRestaurantQueryHandler(ILogger<GetDishesByRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetDishesByRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesByRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying dishes for RestaurantId: {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetOneByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

        return results;

    }
}
