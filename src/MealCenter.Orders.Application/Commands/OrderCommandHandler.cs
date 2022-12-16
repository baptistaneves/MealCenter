using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Orders.Domain;
using MediatR;

namespace MealCenter.Orders.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddMenuOptionToOrderCommand, bool>,
        IRequestHandler<UpdateMenuOptionToOrderCommand, bool>,
        IRequestHandler<RemoveMenuOptionToOrderCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<CloseOrderCommand, bool>
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

            var order = await _orderRepository.GetDraftOrderByClientId(command.ClientId);
            var menuOptionToOrder = new MenuOptionToOrder(command.MenuOptionId, command.ProductId, command.ProductName, command.MenuOptionName, command.ProductPrice, command.MenuOptionPrice, command.ProductQuantity, command.MenuOptionQuantity);

            if(order == null)
            {
                order = Order.OrderFactory.NewDraftOrder(command.ClientId);
                order.AddMenuOptionToOrder(menuOptionToOrder);

                _orderRepository.Add(order);
            }
            else
            {
                var existingMenuOptionToOrder = order.ExistingMenuOptionToOrder(menuOptionToOrder);
                order.AddMenuOptionToOrder(menuOptionToOrder);

                if(existingMenuOptionToOrder)
                {
                    _orderRepository.UpdateMenuOptionToOrder(order.MenuOptionToOrders.FirstOrDefault(m => m.MenuOptionId == menuOptionToOrder.MenuOptionId || m.ProductId == menuOptionToOrder.ProductId));
                }
                else
                {
                    _orderRepository.AddMenuOptionToOrder(menuOptionToOrder);
                }
            }

            return await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<bool> Handle(UpdateMenuOptionToOrderCommand command, CancellationToken cancellationToken)
        {
            if (ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(command.ClientId);

            if(order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var menuOptionToOrder = await _orderRepository.GetMenuOptionToOrderByOrder(command.OrderId, command.MenuOptionId, command.ProductId);

            if(!order.ExistingMenuOptionToOrder(menuOptionToOrder))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Menu option to order not found"));
                return false;
            }

            order.UpdateUnits(menuOptionToOrder, command.MenuOptionQuantity, command.ProductQuantity);

            _orderRepository.UpdateMenuOptionToOrder(menuOptionToOrder);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<bool> Handle(RemoveMenuOptionToOrderCommand command, CancellationToken cancellationToken)
        {
            if (ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(command.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var menuOptionToOrder = await _orderRepository.GetMenuOptionToOrderByOrder(order.Id, command.MenuOptionId, command.ProductId);

            if (menuOptionToOrder == null && !order.ExistingMenuOptionToOrder(menuOptionToOrder))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Menu option to order not found"));
                return false;
            }

            order.RemoveMenuOptionFromOrder(menuOptionToOrder);

            _orderRepository.RemoveMenuOptionToOrder(menuOptionToOrder);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<bool> Handle(StartOrderCommand command, CancellationToken cancellationToken)
        {
            if (ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(command.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            order.StartOrder();

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<bool> Handle(CloseOrderCommand command, CancellationToken cancellationToken)
        {
            if (ValidateCommand(command)) return false;

            var order = await _orderRepository.GetDraftOrderByClientId(command.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            order.CloseOrder();

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.SaveAsync(cancellationToken);
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
