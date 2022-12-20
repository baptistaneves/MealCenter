using Microsoft.AspNetCore.Identity;

namespace MealCenter.Identity.Application.DTOs
{
    public class LoginDto
    {
        public string Role { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
