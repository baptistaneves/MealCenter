using FluentValidation;
using MealCenter.Registration.Application.Contracts.Restaurants;

namespace MealCenter.Registration.Application.Validators.Restaurants
{
    internal class CreateMenuValidator : AbstractValidator<CreateMenu>
    {
        public CreateMenuValidator()
        {
            RuleFor(m => m.RestaurantId)
                .NotEmpty().WithMessage("Restaurant is required")
                .NotEqual(Guid.Empty).WithMessage("Restaraunt ID is not valid");

            RuleFor(m => m.Type)
                .NotEmpty().WithMessage("Type is required");
                
        }
    }

    internal class UpdateMenuValidator : AbstractValidator<UpdateMenu>
    {
        public UpdateMenuValidator()
        {
            RuleFor(m => m.Type)
                .NotEmpty().WithMessage("Type is required");

        }
    }
}
