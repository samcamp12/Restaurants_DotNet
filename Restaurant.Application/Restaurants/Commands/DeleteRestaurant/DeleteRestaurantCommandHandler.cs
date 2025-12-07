using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling DeleteRestaurantCommand for Restaurant Id: {request.Id}");
        var restaurant = await restaurantsRepository.GetOneByIdAsync(request.Id);
        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with Id: {request.Id} not found.");
            return false;
        }

        await restaurantsRepository.DeleteAsync(restaurant);
        return true;
    }
}
