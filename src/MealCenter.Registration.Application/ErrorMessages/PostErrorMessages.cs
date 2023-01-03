namespace MealCenter.Registration.Application.ErrorMessages
{
    internal class PostErrorMessages
    {
        public const string PostTitleAreadyExists = "Provided post title name already exists.";
        public const string PostNotFound = "Post with provided ID was not found.";
        public const string PostCommentNotFound = "Post comment with provided ID was not found.";
        public const string PostReactionNotFound = "Post reation with provided ID was not found.";
        public const string PostDeleteNotNotAuthorized = "Post delete is not possible beacause it's not the post owner";
        public const string PostUpdateNotNotAuthorized = "Post update is not possible beacause it's not the post owner";
        public const string PostCommentRemovelNotNotAuthorized = "Post comment removel is not possible beacause it's not the post owner";
        public const string PostReactionRemovelNotNotAuthorized = "Post reaction removel is not possible beacause it's not the post owner";
    }
}
