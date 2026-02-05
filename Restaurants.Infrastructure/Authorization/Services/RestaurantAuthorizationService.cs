using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail} (ID: {UserId}) for operation {Operation} on restaurant {RestaurantName} (OwnerId: {OwnerId})",
            user?.Email ?? "Anonymous", user?.Id, resourceOperation, restaurant.Name, restaurant.OwnerId);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation - Authorized");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Delete operation by Admin - Authorized");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
            && user.Id == restaurant.OwnerId)
        {
            logger.LogInformation("{Operation} operation by Owner - Authorized", resourceOperation);
            return true;
        }

        logger.LogWarning("Authorization failed - User ID: {UserId}, Restaurant OwnerId: {OwnerId}, Operation: {Operation}",
            user?.Id, restaurant.OwnerId, resourceOperation);
        return false;
    }
}
