namespace MealCenter.API.Contracts.Identity.Requests
{
    public class CreateUserProfileIdentity
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage ="Name must be at least 3 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "The provided email is not valid")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
