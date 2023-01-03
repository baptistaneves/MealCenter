using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Clients
{
    public class UpdateClient
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long")]
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
