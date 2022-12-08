using FluentValidation;
using MealCenter.Registration.Application.Contracts.Restaurants;

namespace MealCenter.Registration.Application.Validators.Restaurants
{
    internal class CreateTableValidator : AbstractValidator<CreateTable>
    {
        public CreateTableValidator()
        {
            RuleFor(t => t.RestaurantId)
                .NotEmpty().WithMessage("Restaurant is required")
                .NotEqual(Guid.Empty).WithMessage("Restaurant ID is not valid");

            RuleFor(t => t.TableNumber)
                .NotEmpty().WithMessage("Table number is required")
                .GreaterThan(0).WithMessage("Table number must be greater than 0");
        }
    }

    internal class UpdateTableValidator : AbstractValidator<UpdateTable>
    {
        public UpdateTableValidator()
        {
            RuleFor(t => t.TableNumber)
                .NotEmpty().WithMessage("Table number is required")
                .GreaterThan(0).WithMessage("Table number must be greater than 0");
        }
    }
}
