using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Restaurants
{
    public class CreateMenu
    {
        [Required(ErrorMessage = "Restaurant is required")]
        public Guid RestaurantId { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }

    public class UpdateMenu
    {
        public bool Status { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }
}
