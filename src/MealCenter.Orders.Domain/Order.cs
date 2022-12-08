using MealCenter.Core.DomainObjects;

namespace MealCenter.Orders.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public Guid ClientId { get; private set; }
        public Guid RestaurantId { get; private set; }
        public Guid TableId { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public decimal Amount { get; private set; }
        public int Code { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        private readonly List<MenuOptionToOrder> _menuOptionToOrders;
        public IReadOnlyCollection<MenuOptionToOrder> MenuOptionToOrders => _menuOptionToOrders;

        public Order(Guid clientId, Guid restaurantId, Guid tableId, decimal amount)
        {
            ClientId = clientId;
            RestaurantId = restaurantId;
            TableId = tableId;
            Amount = amount;
        }

        protected Order() { }

        public void CalculteOrderAmount()
        {
            Amount = MenuOptionToOrders.Sum(m => m.CalculateSubtotal());
        }

        public MenuOptionToOrder MenuOptionAlreadyExistsInOrder(MenuOptionToOrder menuOptionToOrder)
        {           
            if (_menuOptionToOrders.Any(m => m.MenuOptionId == menuOptionToOrder.MenuOptionId))
            {
                var currentMenuOptionInOrder = _menuOptionToOrders
                    .FirstOrDefault(m => m.MenuOptionId == menuOptionToOrder.MenuOptionId);

                currentMenuOptionInOrder.AddUnitsToMenuOptionQuantity(menuOptionToOrder.MenuOptionQuantity);
                menuOptionToOrder = currentMenuOptionInOrder;

                _menuOptionToOrders.Remove(currentMenuOptionInOrder);

            }

            return menuOptionToOrder;
        }

        public MenuOptionToOrder ProductAlreadyExistsInOrder(MenuOptionToOrder menuOptionToOrder)
        {
            if (_menuOptionToOrders.Any(m => m.ProductId == menuOptionToOrder.ProductId))
            {
                var currentMenuOptionInOrder = _menuOptionToOrders
                    .FirstOrDefault(m => m.ProductId == menuOptionToOrder.ProductId);

                currentMenuOptionInOrder.AddUnitsToProductQuantity(menuOptionToOrder.ProductQuantity);
                menuOptionToOrder = currentMenuOptionInOrder;

                _menuOptionToOrders.Remove(currentMenuOptionInOrder);

            }

            return menuOptionToOrder;
        }

        public void AddMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder)
        {
            menuOptionToOrder.AssociateToOrder(Id);

            menuOptionToOrder = MenuOptionAlreadyExistsInOrder(menuOptionToOrder);

            menuOptionToOrder = ProductAlreadyExistsInOrder(menuOptionToOrder);

            _menuOptionToOrders.Add(menuOptionToOrder);

            CalculteOrderAmount();

        }

        public void RemoveMenuOptionFromOrder(MenuOptionToOrder menuOptionToOrder)
        {
            var currentMenuOptionInOrder = _menuOptionToOrders.FirstOrDefault(m => m.MenuOptionId == menuOptionToOrder.MenuOptionId);

            if (currentMenuOptionInOrder != null) throw new DomainException("The menu option does not belong to this order");

            _menuOptionToOrders.Remove(currentMenuOptionInOrder);

            CalculteOrderAmount();
        }

        public void UpdateMenuOptionInOrder(MenuOptionToOrder menuOptionToOrder)
        {
            menuOptionToOrder.AssociateToOrder(Id);

            var currentMenuOptionInOrder = _menuOptionToOrders.FirstOrDefault(m => m.MenuOptionId == menuOptionToOrder.MenuOptionId);

            if (currentMenuOptionInOrder != null) throw new DomainException("The menu option does not belong to this order");

            _menuOptionToOrders.Remove(currentMenuOptionInOrder);
            _menuOptionToOrders.Add(menuOptionToOrder);

            CalculteOrderAmount();
        }

        public void DraftOrder()
        {
            OrderStatus = OrderStatus.Draft;   
        }

        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;

        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;

        }

        public void CloseOrder()
        {
            OrderStatus = OrderStatus.Closed;

        }

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId
                };

                order.DraftOrder();
                return order;
            }
        }

    }
}
