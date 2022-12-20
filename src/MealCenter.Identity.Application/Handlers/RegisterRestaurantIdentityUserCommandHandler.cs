using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Identity.Application.Commands;
using MealCenter.Identity.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MealCenter.Identity.Application.Handlers
{
    public class RegisterRestaurantIdentityUserCommandHandler : IRequestHandler<RegisterRestaurantIdentityUserCommand, IdentityUser>
    {
        private readonly UserManager<IdentityUser> _userManagar;
        private readonly IMediatorHandler _mediatorHandler;
        public RegisterRestaurantIdentityUserCommandHandler(IMediatorHandler mediatorHandler, UserManager<IdentityUser> userManagar)
        {
            _mediatorHandler = mediatorHandler;
            _userManagar = userManagar;
        }

        public async Task<IdentityUser> Handle(RegisterRestaurantIdentityUserCommand command, CancellationToken cancellationToken)
        {
            var newIdentity = await CreateIdentityUser(command.Email, command.Password);

            if (newIdentity == null) return null;

            if (!await AddIdentityUserToRole(newIdentity)) return null;

            return newIdentity;
        }

        private async Task<IdentityUser> CreateIdentityUser(string email, string password)
        {
            var newIdentity = new IdentityUser { Email = email, UserName = email };

            var createUserResult = await _userManagar.CreateAsync(newIdentity, password);

            if (!createUserResult.Succeeded)
            {
                foreach (var error in createUserResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return null;
                }
            }

            return newIdentity;
        }

        private async Task<bool> AddIdentityUserToRole(IdentityUser identity)
        {
            var addToRoleResult = await _userManagar.AddToRoleAsync(identity, UserRoles.RESTAURANT);

            if (!addToRoleResult.Succeeded)
            {
                foreach (var error in addToRoleResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return false;
                }
            }

            return true;
        }
    }
}
