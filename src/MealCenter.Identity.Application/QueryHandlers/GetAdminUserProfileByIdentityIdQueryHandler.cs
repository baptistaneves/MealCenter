using MealCenter.Identity.Application.Queries;
using MealCenter.Identity.Domain;
using MealCenter.Identity.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Identity.Application.QueryHandlers
{
    public class GetAdminUserProfileByIdentityIdQueryHandler : IRequestHandler<GetAdminUserProfileByIdentityIdQuery, UserProfile>
    {
        private readonly IdentityContext _context;

        public GetAdminUserProfileByIdentityIdQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> Handle(GetAdminUserProfileByIdentityIdQuery request, CancellationToken cancellationToken)
        {
           var userProfile = await _context.UserProfiles.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (userProfile == null) return null;

            return userProfile;
        }
    }
}
