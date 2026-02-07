using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandler(ILogger<CreateMultipleRestaurantsRequirementHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
        {
            logger.LogWarning("User context is not available. Authorization failed.");
            context.Fail();
            return;
        }

        var restaurantList = await restaurantsRepository.GetAllAsync();
        var restaurantCreatedByCurrentUser = restaurantList.Count(r => r.OwnerId == currentUser.Id);

        if (restaurantCreatedByCurrentUser >= requirement.MinimumRestaurantAmount)
        {
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("User {UserId} has created {RestaurantCount} restaurants, which does not meet the requirement of more than {MinimumAmount}. Authorization failed.",
                currentUser.Id, restaurantCreatedByCurrentUser, requirement.MinimumRestaurantAmount);
            context.Fail();
        }
    }
}
