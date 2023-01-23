using MealCenter.Core.Messages;
using MealCenter.Orders.Application.Commands.Validators;

namespace MealCenter.Orders.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public StartOrderCommand(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
