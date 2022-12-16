namespace MealCenter.Orders.Application.Queries.ViewModels
{
    public class MenuOptionToOrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid MenuOptionId { get; set; }
        public Guid ProductId { get; set; }
        public string MenuOptionName { get; set; }
        public string ProductName { get; set; }
        public decimal MenuOptionPrice { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductQuantity { get; set; }
        public int MenuOptionQuantity { get; set; }
    }
}
