using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class RemoveMenuOptionToOrderCommand : Command
    {
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid MenuOptionId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveMenuOptionToOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
