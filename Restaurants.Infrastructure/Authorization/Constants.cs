namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationlity = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string MinimumCreatedRestaurants = "MinimumCreatedRestaurants";
}

public static class AppClaimTypes
{
    public const string Nationlity = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
}


