using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MealCenter.Identity.Application.Commands
{
    public class LoginCommand : IRequest<IdentityUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
