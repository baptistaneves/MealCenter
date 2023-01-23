using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class CloseOrderCommand : Command
    {
        public CloseOrderCommand(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CloseOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
