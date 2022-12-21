using MealCenter.Identity.Domain;
using MediatR;

namespace MealCenter.Identity.Application.Queries
{
    public class GetAllAdminUserProfilesQuery : IRequest<List<UserProfile>> { }
}
