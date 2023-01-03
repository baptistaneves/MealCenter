namespace MealCenter.API.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetUserProfileIdCliamValue(this HttpContext context)
        {
            return GetGuidClaimValue("UserProfileId", context);
        }

        public static Guid GetIdentityIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("IdentityId", context);
        }

        public static Guid GetClientIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("ClientId", context);
        }

        public static Guid GetRestaurantIdClaimValue(this HttpContext context)
        {
            return GetGuidClaimValue("RestaurantId", context);
        }

        private static Guid GetGuidClaimValue(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return Guid.Parse(identity?.FindFirst(key)?.Value);
        }
    }
}
