using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Identity.Application.Commands;
using MealCenter.Identity.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MealCenter.Identity.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediatorHandler _mediatorHandler;

        public LoginCommandHandler(UserManager<IdentityUser> userManager, 
                                   SignInManager<IdentityUser> signInManager, IMediatorHandler mediatorHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<LoginDto> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var identity = await CheckEmail(command.Email);

            if (identity is null) return null;

            if (!await CheckPassword(identity, command.Password)) return null;

            var roles = await _userManager.GetRolesAsync(identity);

            return new LoginDto { IdentityUser = identity, Role = roles.FirstOrDefault()};
        }

        private async Task<IdentityUser> CheckEmail(string email)
        {
            var identity = await _userManager.FindByEmailAsync(email);

            if (identity is null)
               await _mediatorHandler.PublishNotification(new DomainNotification("Identity", "Wrong username or password. Login failed."));

            return identity;
        }

        private async Task<bool> CheckPassword(IdentityUser identity, string password)
        {
            var validatePassword = await _signInManager.CheckPasswordSignInAsync(identity, password, true);

            if (validatePassword.IsLockedOut)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Identity", "User temporarily blocked for invalid attempts."));
                return false;
            }

            if (!validatePassword.Succeeded)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Identity", "Wrong username or password. Login failed."));
                return false;
            }

            return true;
        }
    }
}
