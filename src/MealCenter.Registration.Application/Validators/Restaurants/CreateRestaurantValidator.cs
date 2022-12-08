using FluentValidation;
using MealCenter.Registration.Application.Contracts.Restaurants;

namespace MealCenter.Registration.Application.Validators.Restaurants
{
    internal class CreateRestaurantValidator : AbstractValidator<CreateRestaurant>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(4).WithMessage("Name must have at least 4 character");

            RuleFor(r => r.Location)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(10).WithMessage("Name must be at least 10 character");
        }
    }

    internal class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurant>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(4).WithMessage("Name must have at least 4 character");

            RuleFor(r => r.Location)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(10).WithMessage("Name must be at least 10 character");
        }
    }
}
