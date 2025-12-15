using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.KiloCalorities)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalorities must be greater than or equal to 0")
            .When(x => x.KiloCalorities.HasValue);
    }
}
