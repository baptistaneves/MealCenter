using FluentValidation;

namespace MealCenter.Orders.Application.Commands.Validators
{
    internal class UpdateMenuOptionToOrderCommandValidator : AbstractValidator<UpdateMenuOptionToOrderCommand>
    {
        public UpdateMenuOptionToOrderCommandValidator()
        {
            RuleFor(o => o.OrderId).NotEqual(Guid.Empty).WithMessage("The ID order is invalid");

            RuleFor(o => o.ClientId).NotEqual(Guid.Empty).WithMessage("The ID client is invalid");

            When(o => o.MenuOptionId != Guid.Empty, () =>
            {
                RuleFor(o => o.MenuOptionQuantity)
                    .GreaterThan(0).WithMessage("Menu option quantity must greater than 0");
            });

            When(o => o.ProductId != Guid.Empty, () =>
            {
                RuleFor(o => o.ProductQuantity)
                    .GreaterThan(0).WithMessage("Product quantity must greater than 0");
            });

            When(o => o.ProductId == Guid.Empty && o.MenuOptionId == Guid.Empty, () =>
            {
                RuleFor(o => o.MenuOptionId)
                    .NotEqual(Guid.Empty).WithMessage("Product or Menu Option must be informed to proceed with the order");
            });
        }
    }
}
