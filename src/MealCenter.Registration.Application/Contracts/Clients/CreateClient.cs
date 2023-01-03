using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Clients
{
    public class CreateClient
    {
        [Required(ErrorMessage = "First name is required")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(3, ErrorMessage = "Last name must be at least 3 characters long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
