using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext
        ) : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
    
            var currentUser = userContext.GetCurrentUser();

            if (currentUser == null)
            {
                logger.LogWarning("User context is not available. Authorization failed.");
                context.Fail();
                return Task.CompletedTask;
            }

            logger.LogInformation("Evaluating MinimumAgeRequirement for user {Email} with minimum age {MinimumAge}", currentUser.Email, requirement.MinimumAge);

            if (currentUser.DateOfBirth == null)
            {
                logger.LogWarning("Authorization failed for user {UserId} due to missing DateOfBirth", currentUser.Id);
                context.Fail();
                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization succeeded for user {UserId} with minimum age {MinimumAge}", currentUser.Id, requirement.MinimumAge);
                context.Succeed(requirement);

            }
            else
            {
                logger.LogWarning("Authorization failed for user {UserId} with minimum age {MinimumAge}", currentUser.Id, requirement.MinimumAge);
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
