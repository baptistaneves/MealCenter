namespace MealCenter.Orders.Application.Queries.ViewModels
{
    public class MyCartViewModel
    {
        public Guid ProductId { get; set; }
        public Guid MenuOptionId { get; set; }
        public int ProductQuantity { get; set; }
        public int MenuOptionQuantity { get; set; }
    }

    public class RemoveItemViewModel
    {
        public Guid ClientId { get; set; }
        public Guid ProductId { get; set; }
        public Guid MenuOptionId { get; set; }
    }

    public class UpdateItemViewModel
    {
        public Guid ProductId { get; set; }
        public Guid MenuOptionId { get; set; }
        public int ProductQuantity { get; set; }
        public int MenuOptionQuantity { get; set; }
    }
}
