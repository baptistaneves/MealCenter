using FluentValidation;

namespace MealCenter.Orders.Application.Commands.Validators
{
    internal class CloseOrderCommandValidator : AbstractValidator<CloseOrderCommand>
    {
        public CloseOrderCommandValidator()
        {
            RuleFor(o => o.ClientId).NotEqual(Guid.Empty).WithMessage("The ID client is invalid");
        }
    }
}
