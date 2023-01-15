namespace MealCenter.API
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:ApiVersion}/[controller]";

        public static class Client
        {
            public const string GetClientById = "get-client-by-id/{id}";
            public const string GetTheNumberOfRegisteredClients = "get-number-of-registered-clients";
            public const string ActivateClient = "activate-client-by-id/{id}";
            public const string DeactivateClient = "deactivate-client-by-id/{id}";
            public const string AddClient = "add-client";
            public const string RemoveClient = "remove-client/{id}";
            public const string UpdateClient = "update-client/{id}";
        }

        public static class Restaurant
        {
            public const string GetAllMenu = "get-all-menu";
            public const string GetAllMenuOption = "get-all-menu-option";
            public const string GetMenuOptionsByMenuId = "get-menu-options-by-menu-id/{menuId}";
            public const string GetMenusByRestaurantId = "get-menus-by-restaurant-id/{restaurantId}";
            public const string GetTablesByRestaurantId = "get-tables-by-restaurant-id/{restaurantId}";
            public const string GetAllTable = "get-all-table";

            public const string GetById = "get-restaurant-by-id/{id}";
            public const string GetMenuById = "get-menu-by-id/{id}";
            public const string GetMenuOptionById = "get-menu-option-by-id/{id}";
            public const string GetTableById = "get-table-by-id/{id}";

            public const string RemoveRestaurant = "remove-restaurant/{id}";
            public const string RemoveMenu = "remove-menu/{id}";
            public const string RemoveMenuOption = "remove-menu-option/{id}";
            public const string RemoveTable = "remove-table/{id}";

            public const string GetTheNumberOfRegisteredRestaurants = "get-number-of-registered-restaurants";

            public const string FreeTable = "free-table/{id}";
            public const string OccupyTable = "{id}/occupy-table/{clientId}";

            public const string ActivateRestaurant = "activate-restaurant/{id}";
            public const string DeactivateRestaurant = "deactivate-restaurant/{id}";

            public const string AddRestaurant = "add-restaurant";
            public const string AddMenu = "add-menu";
            public const string AddMenuOption = "add-menu-option";
            public const string AddTable = "add-table";

            public const string UpdateRestaurant = "update-restaurant/{id}";
            public const string UpdateMenu = "update-menu/{id}";
            public const string UpdateMenuOption = "update-menu-option/{id}";
            public const string UpdateTable = "update-table/{id}";
        }

        public static class Identity
        {
            public const string CreateAdminUserProfileIdentity = "create-user-admin";
            public const string GetAdminUserProfileById = "get-user-admin-profile-by-id/{id}";
            public const string LoginForAdminUser = "user-admin-login";
            public const string LoginForClient = "client-login";
            public const string LoginForRestaurant = "restaurant-login";
        }

        public static class Post
        {
            public const string GetAllPostComments = "get-all-post-comments/{postId}";
            public const string GetAllPostReaction = "get-all-post-reactions/{postId}";
            public const string GetPostById = "get-post-by-id/{id}";
            public const string GetPostCommentById = "get-post-comment-by-id/{id}";

            public const string RemovePost = "remove-post/{id}";
            public const string RemovePostComment = "{postId}/remove-comment-from-post/{postCommentId}";
            public const string RemovePostReaction = "{postId}/remove-comment-from-post/{postReactionId}";

            public const string AddPost = "add-post";
            public const string AddPostComment = "add-post-comment";
            public const string AddPostReaction = "add-post-reaction";

            public const string UpdatePost = "update-post/{id}";
            public const string UpdatePostComment = "{postId}/add-post-comment/{postCommentId}";
        }
    }
}
