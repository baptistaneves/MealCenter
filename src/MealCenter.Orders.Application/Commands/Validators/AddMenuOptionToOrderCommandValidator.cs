using FluentValidation;

namespace MealCenter.Orders.Application.Commands.Validators
{
    internal class AddMenuOptionToOrderCommandValidator : AbstractValidator<AddMenuOptionToOrderCommand>
    {
        public AddMenuOptionToOrderCommandValidator()
        {
            RuleFor(o => o.ClientId)
                .NotEqual(Guid.Empty).WithMessage("The client ID is not valid");

            RuleFor(o => o.RestaurantId)
                .NotEqual(Guid.Empty).WithMessage("The restaurant ID is not valid");

            RuleFor(o => o.TableId)
                .NotEqual(Guid.Empty).WithMessage("The table ID is not valid");

            When(o => o.ProductId != Guid.Empty, () =>
            {
                RuleFor(o => o.ProductPrice)
                    .NotEmpty().WithMessage("The price of product is required")
                    .GreaterThan(1).WithMessage("The price of product must be greater than 0");

                RuleFor(o => o.ProductQuantity)
                    .NotEmpty().WithMessage("The quantity of product is required")
                    .GreaterThan(1).WithMessage("The quantity of product must be greater than 0");

                RuleFor(o => o.ProductName)
                    .NotEmpty().WithMessage("The name of product is required");


            });

            When(o => o.MenuOptionId != Guid.Empty, () =>
            {
                RuleFor(o => o.MenuOptionPrice)
                    .NotEmpty().WithMessage("The price of menu option is required")
                    .GreaterThan(1).WithMessage("The price of menu option must be greater than 0");

                RuleFor(o => o.MenuOptionQuantity)
                    .NotEmpty().WithMessage("The quantity of menu option is required")
                    .GreaterThan(1).WithMessage("The quantity of product must be greater than 0");

                RuleFor(o => o.MenuOptionName)
                    .NotEmpty().WithMessage("The name of menu option is required");

            });

            When(o => o.ProductId == Guid.Empty && o.MenuOptionId == Guid.Empty, () =>
            {
                RuleFor(o => o.MenuOptionId)
                    .NotEqual(Guid.Empty).WithMessage("A product or menu option must be informed to proceed with the order");
            });
        }
    }
}
