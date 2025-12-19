using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesFromRestaurantCommandHandler(ILogger<DeleteDishesFromRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository
    ) : IRequestHandler<DeleteDishesFromRestaurantCommand>
{
    public async Task Handle(DeleteDishesFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with RestaurantId: {RestaurantId}", request.RestaurantId);
        var restaurant = await restaurantsRepository.GetOneByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        await dishesRepository.DeleteAsync(restaurant.Dishes);
    }
}
