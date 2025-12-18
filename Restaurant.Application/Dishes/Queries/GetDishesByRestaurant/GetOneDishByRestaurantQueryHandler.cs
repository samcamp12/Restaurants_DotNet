using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesByRestaurant;

public class GetOneDishByRestaurantQueryHandler(ILogger<GetOneDishByRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetOneDishByRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetOneDishByRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying dishfor RestaurantId: {RestaurantId}, DishId: {DishId}", request.RestaurantId, request.DishId);
        var restaurant = await restaurantsRepository.GetOneByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dish = restaurant.Dishes?.FirstOrDefault(d => d.Id == request.DishId) ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        var result = mapper.Map<DishDto>(dish);

        return result;
    }
}
