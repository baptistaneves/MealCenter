using FluentValidation;

namespace MealCenter.Orders.Application.Commands.Validators
{
    internal class StartOrderCommandValidator : AbstractValidator<StartOrderCommand>
    {
        public StartOrderCommandValidator()
        {
            RuleFor(o => o.ClientId).NotEqual(Guid.Empty).WithMessage("The ID client is invalid");
        }
    }
}
