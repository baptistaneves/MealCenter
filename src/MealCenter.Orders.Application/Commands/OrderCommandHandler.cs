using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Orders.Domain;
using MediatR;

namespace MealCenter.Orders.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddMenuOptionToOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddMenuOptionToOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;


        }

        private bool ValidateCommand(Command command)
        {
            if(command.IsValid()) return true;

            foreach(var error in command.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification("order", error.ErrorMessage));
            }

            return false;
        }
    }
}
