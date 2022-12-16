using MediatR;

namespace MealCenter.Identity.Application.Commands
{
    public class RegisterRestaurantIdentityCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
