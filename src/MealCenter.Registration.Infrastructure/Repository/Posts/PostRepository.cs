using MealCenter.Core.Data;
using MealCenter.Registration.Domain.Posts;
using MealCenter.Registration.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Registration.Infrastructure.Repository.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly RegistrationContext _context;

        public PostRepository(RegistrationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Post entity)
        {
            _context.Posts.Add(entity);
        }

        public void Update(Post entity)
        {
            _context.Posts.Update(entity);
        }

        public void Remove(Post entity)
        {
            _context.Posts.Remove(entity);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.AsNoTracking().ToListAsync();
        }

        public async Task<Post> GetById(Guid id)
        {
            return await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> PostAlreadyExists(string title)
        {
            return await _context.Posts.AsNoTracking().Where(p => p.Title == title).AnyAsync();
        }

        public async Task<bool> PostAlreadyExists(Guid id, string title)
        {
            return await _context.Posts.AsNoTracking().Where(p => p.Title == title && p.Id != id).AnyAsync();
        }

        public void AddPostComment(PostComment comment)
        {
            _context.PostComments.Add(comment);
        }

        public void UpdatePostComment(PostComment postComment)
        {
            _context.PostComments.Update(postComment);
        }

        public void RemovePostComment(PostComment postComment)
        {
            _context.PostComments.Remove(postComment);
        }

        public async Task<IEnumerable<PostComment>> GetAllPostComment()
        {
            return await _context.PostComments.AsNoTracking().ToListAsync();
        }

        public async Task<PostComment> GetPostCommentById(Guid id)
        {
            return await _context.PostComments.AsNoTracking().FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public void AddPostReaction(PostReaction reaction)
        {
            _context.PostReactions.Add(reaction);
        }

        public void RemovePostReaction(PostReaction reaction)
        {
            _context.PostReactions.Remove(reaction);
        }

        public async Task<IEnumerable<PostReaction>> GetAllPostReaction()
        {
            return await _context.PostReactions.AsNoTracking().ToListAsync();
        }

        public async Task<PostReaction> GetPostReactionById(Guid id)
        {
            return await _context.PostReactions.AsNoTracking().FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
