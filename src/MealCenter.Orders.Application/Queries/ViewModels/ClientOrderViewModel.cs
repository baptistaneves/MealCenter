namespace MealCenter.Orders.Application.Queries.ViewModels
{
    public class ClientOrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid TableId { get; set; }
        public decimal Amount { get; set; }

        public List<MenuOptionToOrderViewModel> MenuOptionsToOrder { get; set; } = new List<MenuOptionToOrderViewModel>();  
    }
}
