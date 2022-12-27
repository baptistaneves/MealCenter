using MealCenter.Identity.Application.Queries;
using MealCenter.Identity.Domain;
using MealCenter.Identity.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Identity.Application.QueryHandlers
{
    public class GetAllAdminUserProfilesQueryHandler : IRequestHandler<GetAllAdminUserProfilesQuery, List<UserProfile>>
    {
        private readonly IdentityContext _context;

        public GetAllAdminUserProfilesQueryHandler(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfile>> Handle(GetAllAdminUserProfilesQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserProfiles.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
