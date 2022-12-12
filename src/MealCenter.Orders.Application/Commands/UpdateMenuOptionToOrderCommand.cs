using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class UpdateMenuOptionToOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid MenuOptionId { get; private set; }
        public int ProductQuantity { get; private set; }
        public int MenuOptionQuantity { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateMenuOptionToOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
