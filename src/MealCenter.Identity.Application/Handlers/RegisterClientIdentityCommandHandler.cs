using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Identity.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MealCenter.Identity.Application.Handlers
{
    public class RegisterClientIdentityCommandHandler : IRequestHandler<RegisterClientIdentityCommand, string>
    {
        private readonly UserManager<IdentityUser> _userManagar;
        private readonly IMediatorHandler _mediatorHandler;
        public RegisterClientIdentityCommandHandler(IMediatorHandler mediatorHandler, UserManager<IdentityUser> userManagar)
        {
            _mediatorHandler = mediatorHandler;
            _userManagar = userManagar;
        }

        public async Task<string> Handle(RegisterClientIdentityCommand request, CancellationToken cancellationToken)
        {
            var newIdentity = new IdentityUser { Email = request.Email, UserName = request.Email };

            var createUserResult = await _userManagar.CreateAsync(newIdentity, request.Password);

            if(!createUserResult.Succeeded)
            {
                foreach (var error in createUserResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return string.Empty;
                }
            }

            var addToRoleResult = await _userManagar.AddToRoleAsync(newIdentity, "Client");

            if (!addToRoleResult.Succeeded)
            {
                foreach (var error in addToRoleResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return string.Empty;
                }
            }

            return newIdentity.Id;
        }
    }
    
}
