using MealCenter.Core.Data;

namespace MealCenter.Registration.Domain.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(Guid id);
        Task<bool> PostAlreadyExists(string title);
        Task<bool> PostAlreadyExists(Guid id, string title);
        void Add(Post entity);
        void Update(Post entity);
        void Remove(Post entity);

        Task<PostComment> GetPostCommentById(Guid id);
        Task<IEnumerable<PostComment>> GetAllPostComment();
        void AddPostComment(PostComment comment); 
        void UpdatePostComment(PostComment postComment);
        void RemovePostComment(PostComment postComment);

        Task<PostReaction> GetPostReactionById(Guid id);
        Task<IEnumerable<PostReaction>> GetAllPostReaction();
        void AddPostReaction(PostReaction reaction);
        void RemovePostReaction(PostReaction reaction);
    }
}
