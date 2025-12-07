using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters")
            .When(x => x.Name != null);
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty")
            .When(x => x.Description != null);
    }
}
