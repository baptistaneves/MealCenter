using MealCenter.Orders.Domain;

namespace MealCenter.Orders.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid TableId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Amount { get; set; }
        public int Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
