using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class RemoveMenuOptionToOrderCommand : Command
    {
        public RemoveMenuOptionToOrderCommand(Guid clientId, Guid productId, Guid menuOptionId)
        {
            ClientId = clientId;
            ProductId = productId;
            MenuOptionId = menuOptionId;
        }

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
