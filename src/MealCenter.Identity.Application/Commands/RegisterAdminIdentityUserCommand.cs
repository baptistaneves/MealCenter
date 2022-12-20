using MealCenter.Identity.Application.DTOs;
using MediatR;

namespace MealCenter.Identity.Application.Commands
{
    public class RegisterAdminIdentityUserCommand : IRequest<IdentityUserProfileDto>
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
