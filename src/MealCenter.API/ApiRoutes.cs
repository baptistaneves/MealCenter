namespace MealCenter.API
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:ApiVersion}/[controller]";

        public static class Client
        {

        }

        public static class Identity
        {
            public const string CreateAdminUserProfileIdentity = "create-user-admin";
            public const string GetAdminUserProfileIdentityById = "get-user-admin-profile-by-id/{id}";
        }
    }
}
