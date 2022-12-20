using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Identity.Application.Commands;
using MealCenter.Identity.Application.Common;
using MealCenter.Identity.Application.DTOs;
using MealCenter.Identity.Application.Services;
using MealCenter.Identity.Domain;
using MealCenter.Identity.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace MealCenter.Identity.Application.Handlers
{
    public class RegisterAdminIdentityUserCommandHandler : IRequestHandler<RegisterAdminIdentityUserCommand, IdentityUserProfileDto>
    {
        private readonly IdentityContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly JwtAdminService _jwtAdminService;

        public RegisterAdminIdentityUserCommandHandler(IdentityContext context,
                                                       UserManager<IdentityUser> userManager,
                                                       IMediatorHandler mediatorHandler,
                                                       JwtService jwtService, 
                                                       JwtAdminService jwtAdminService)
        {
            _context = context;
            _userManager = userManager;
            _mediatorHandler = mediatorHandler;
            _jwtAdminService = jwtAdminService;
        }

        public async Task<IdentityUserProfileDto> Handle(RegisterAdminIdentityUserCommand command, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var identity = await CreateIdentityUserAsync(command.EmailAddress, command.Password, transaction, cancellationToken);
            if (identity == null) return null;

            if(!await AddIdentityUserToRole(identity, transaction, cancellationToken))
                return null;

            var userProfile = await CreateUserProfile(command, transaction, identity, cancellationToken);

            return new IdentityUserProfileDto
            {
                IdentityId = identity.Id,
                Name = userProfile.Name,
                EmailAddress = userProfile.EmailAddress,
                Phone = userProfile.Phone,
                Token = await _jwtAdminService.GetJwtString(identity, userProfile)
            };
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(string email, string password,
            IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = email, UserName = email };

            var createdIdentity = await _userManager.CreateAsync(identity, password);

            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var error in createdIdentity.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return null;
                }

            }

            return identity;
        }

        private async Task<bool> AddIdentityUserToRole(IdentityUser identity, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var addToRoleResult = await _userManager.AddToRoleAsync(identity, UserRoles.ADMINISTRATOR);

            if (!addToRoleResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var error in addToRoleResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification("Identity", error.Description));
                    return false;
                }
            }
            return true;
        }

        private async Task<UserProfile> CreateUserProfile(RegisterAdminIdentityUserCommand command, IDbContextTransaction transaction,
            IdentityUser identity, CancellationToken cancellationToken)
        {
            var userProfile = new UserProfile(identity.Id, command.Name, command.EmailAddress, command.Phone);

            _context.UserProfiles.Add(userProfile);

            if(!await _context.SaveAsync(cancellationToken))
                await transaction.RollbackAsync(cancellationToken);

            return userProfile;
                
        }


        
    }
}
