using MealCenter.Identity.Application.DTOs;
using MediatR;

namespace MealCenter.Identity.Application.Commands
{
    public class LoginCommand : IRequest<LoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
