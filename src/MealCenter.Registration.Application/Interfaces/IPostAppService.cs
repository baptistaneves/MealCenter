using MealCenter.Registration.Application.Contracts.Posts;
using MealCenter.Registration.Domain.Posts;

namespace MealCenter.Registration.Application.Interfaces
{
    public interface IPostAppService : IDisposable
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(Guid id);
        Task Add(CreatePost newPost, CancellationToken cancellationToken);
        Task Update(UpdatePost post, CancellationToken cancellationToken);
        Task Remove(Guid id, CancellationToken cancellationToken);

        Task<PostComment> GetPostCommentById(Guid id);
        Task<IEnumerable<PostComment>> GetAllPostComment();
        Task AddPostComment(CreatePostComment newComment, string identityUserId, CancellationToken cancellationToken);
        Task UpdatePostComment(UpdatePostComment postComment, CancellationToken cancellationToken);
        Task RemovePostComment(Guid id, CancellationToken cancellationToken);

        Task<PostReaction> GetPostReactionById(Guid id);
        Task<IEnumerable<PostReaction>> GetAllPostReaction();
        Task AddPostReaction(CreatePostReaction reaction, string identityUserId, CancellationToken cancellationToken);
        Task RemovePostReaction(Guid id, CancellationToken cancellationToken);
    }
}
