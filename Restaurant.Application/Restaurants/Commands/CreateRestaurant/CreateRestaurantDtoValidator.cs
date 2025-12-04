using FluentValidation;
namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "American", "Fast Food", "Find Dining", "Indian", "Japanese"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required")
            .Custom((value, context) =>
            {
                var isValidCategory = validCategories.Contains(value);
                if (!isValidCategory)
                {
                    context.AddFailure("Category", "Invalid category.");
                }
            });
        RuleFor(x => x.ContactEmail)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.ContactEmail))
            .WithMessage("Please provide a valid email address");
        RuleFor(x => x.ContactNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Please provide a valid phone number");
    }
}

