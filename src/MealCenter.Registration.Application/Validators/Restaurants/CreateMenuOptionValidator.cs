using FluentValidation;
using MealCenter.Registration.Application.Contracts.Restaurants;

namespace MealCenter.Registration.Application.Validators.Restaurants
{
    internal class CreateMenuOptionValidator : AbstractValidator<CreateMenuOption>
    {
        public CreateMenuOptionValidator()
        {
            RuleFor(mo => mo.MenuId)
                .NotEmpty().WithMessage("Menu is required")
                .NotEqual(Guid.Empty).WithMessage("Menu ID is not Valid");


            RuleFor(mo => mo.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(4).WithMessage("Name must be at least 4 characters");

            RuleFor(mo => mo.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(mo => mo.Ingredients)
                .NotEmpty().WithMessage("Ingredients is required");
        }
    }

    internal class UpdateMenuOptionValidator : AbstractValidator<UpdateMenuOption>
    {
        public UpdateMenuOptionValidator()
        {
            RuleFor(mo => mo.MenuId)
                .NotEmpty().WithMessage("Menu is required")
                .NotEqual(Guid.Empty).WithMessage("Menu ID is not Valid");


            RuleFor(mo => mo.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(4).WithMessage("Name must be at least 4 characters");

            RuleFor(mo => mo.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(mo => mo.Ingredients)
                .NotEmpty().WithMessage("Ingredients is required");
        }
    }
}
