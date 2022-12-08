using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Restaurants
{
    public class CreateMenuOption
    {
        [Required(ErrorMessage = "Menu is Required")]
        public Guid MenuId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Ingredients is required")]
        public string Ingredients { get; set; }
    }

    public class UpdateMenuOption
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Menu is Required")]
        public Guid MenuId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "Ingredients is required")]
        public string Ingredients { get; set; }
    }
}
