using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class UpdateMenuOptionToOrderCommand : Command
    {
        public UpdateMenuOptionToOrderCommand(Guid clientId, Guid productId, Guid menuOptionId, int productQuantity, int menuOptionQuantity)
        {
            ClientId = clientId;
            ProductId = productId;
            MenuOptionId = menuOptionId;
            ProductQuantity = productQuantity;
            MenuOptionQuantity = menuOptionQuantity;
        }

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
