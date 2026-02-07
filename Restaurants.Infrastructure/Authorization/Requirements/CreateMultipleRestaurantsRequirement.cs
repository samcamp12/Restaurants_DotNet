using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirement(int minimumRestaurantAmount) : IAuthorizationRequirement
{
    public int MinimumRestaurantAmount { get; } = minimumRestaurantAmount;
}
