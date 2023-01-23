using System.ComponentModel.DataAnnotations;

namespace MealCenter.Catalog.Application.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Name must not be empty")]
        [MinLength(4, ErrorMessage = "Name must be at leats 4 characters")]
        public string Name { get; set; }

        public string Description { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater then 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

    }

    public class UpdateProductViewModel
    {
       
        [Required(ErrorMessage = "Name must not be empty")]
        [MinLength(4, ErrorMessage = "Name must be at leats 4 characters")]
        public string Name { get; set; }

        public string Description { get; set; }
        public bool Status { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater then 0")]
        public decimal Price { get; set; }

    }
}
