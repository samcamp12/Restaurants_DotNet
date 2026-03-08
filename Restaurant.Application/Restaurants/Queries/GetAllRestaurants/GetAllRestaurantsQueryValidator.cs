using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;


namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowPageSizes = [5, 10, 15, 30];
    private readonly string[] allowedSortBuColumnNames = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1");
        RuleFor(x => x.PageSize)
                .Must(x => allowPageSizes.Contains(x)).WithMessage($"Page size must be one of the following: {string.Join(", ", allowPageSizes)}");
        RuleFor(r => r.SortBy)
            .Must(value => allowedSortBuColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort is optional, or must me in {string.Join(", ", allowedSortBuColumnNames)}");
    }
}
