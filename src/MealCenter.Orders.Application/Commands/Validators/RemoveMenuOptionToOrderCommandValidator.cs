using FluentValidation;

namespace MealCenter.Orders.Application.Commands.Validators
{
    internal class RemoveMenuOptionToOrderCommandValidator : AbstractValidator<RemoveMenuOptionToOrderCommand>
    {
        public RemoveMenuOptionToOrderCommandValidator()
        {
            RuleFor(o => o.ClientId).NotEqual(Guid.Empty).WithMessage("The ID client is invalid");

            When(o => o.MenuOptionId == Guid.Empty && o.ProductId == Guid.Empty, () =>
            {
                RuleFor(o => o.MenuOptionId)
                    .NotEqual(Guid.Empty).WithMessage("The ID of menu optpion or product must be informed");
            });
        }
    }
}
