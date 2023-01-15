using MealCenter.Registration.Application.Contracts.Posts;
using MealCenter.Registration.Domain.Posts;

namespace MealCenter.Registration.Application.Interfaces
{
    public interface IPostAppService : IDisposable
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(Guid id);
        Task<Post> Add(CreatePost newPost, CancellationToken cancellationToken);
        Task Update(Guid postId, Guid restaurantId, UpdatePost postUpdated, CancellationToken cancellationToken);
        Task Remove(Guid id, Guid restaurantId, CancellationToken cancellationToken);

        Task<PostComment> GetPostCommentById(Guid id);
        Task<IEnumerable<PostComment>> GetAllPostComment(Guid postId);
        Task<PostComment> AddPostComment(CreatePostComment newComment, string identityUserId, CancellationToken cancellationToken);
        Task UpdatePostComment(Guid postCommentId, Guid postId, Guid restaurantId, string identityId, UpdatePostComment postCommentUpdated, CancellationToken cancellationToken);
        Task RemovePostCommentFromPost(Guid postCommentId, Guid postId, Guid restaurantId, string identityId, CancellationToken cancellationToken);

        Task<PostReaction> GetPostReactionById(Guid id);
        Task<IEnumerable<PostReaction>> GetAllPostReaction(Guid postId);
        Task<PostReaction> AddPostReaction(CreatePostReaction reaction, string identityUserId, CancellationToken cancellationToken);
        Task RemovePostReactionFromPost(Guid postReactionId, Guid postId, Guid restaurantId, string identityId, CancellationToken cancellationToken);
    }
}
