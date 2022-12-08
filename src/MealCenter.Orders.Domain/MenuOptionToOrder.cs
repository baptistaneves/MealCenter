using MealCenter.Core.DomainObjects;

namespace MealCenter.Orders.Domain
{
    public class MenuOptionToOrder : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid MenuOptionId { get; private set; }
        public Guid ProductId { get; private set; }
        public string MenuOptionName { get; private set; }
        public string ProductName { get; private set; }
        public decimal MenuOptionPrice { get; private set; }
        public decimal ProductPrice { get; private set; }
        public decimal Subtotal { get; private set; }  
        public int ProductQuantity { get; private set; }
        public int MenuOptionQuantity { get; private set; }

        //EF Rel.
        public Order Order { get; private set; }

        public MenuOptionToOrder(Guid orderId, Guid menuOptionId, Guid productId, string productName,
            string menuOptionName, decimal productPrice, decimal menuOptionPrice, int productQuantity, int menuOptionQuantity)
        {
            OrderId = orderId;
            MenuOptionId = menuOptionId;
            ProductId = productId;
            ProductName = productName;
            MenuOptionName = menuOptionName;
            MenuOptionPrice = menuOptionPrice;
            ProductPrice = productPrice;
            MenuOptionQuantity = menuOptionQuantity;
            ProductPrice = productPrice;
        }

        protected MenuOptionToOrder() { }

        public void AssociateToOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateSubtotal()
        {
            return (MenuOptionPrice * MenuOptionQuantity) + (ProductPrice * ProductPrice);
        }

        public void AddUnitsToMenuOptionQuantity(int units)
        {
            MenuOptionQuantity += units;
        }

        public void AddUnitsToProductQuantity(int units)
        {
            ProductQuantity += units;
        }

        public void UpdateMenuOptionQuantity(int units)
        {
            MenuOptionQuantity = units;
        }

        public void UpdateProductQuantity(int units)
        {
            ProductQuantity = units;
        }
    }
}
