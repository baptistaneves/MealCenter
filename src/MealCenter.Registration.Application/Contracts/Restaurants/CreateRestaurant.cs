using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Restaurants
{
    public class CreateRestaurant
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must have at least 4 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MinLength(10, ErrorMessage = "Location must be at least 10 characters")]
        public string Location { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateRestaurant
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must have at least 4 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MinLength(10, ErrorMessage = "Location must be at least 10 characters")]
        public string Location { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
