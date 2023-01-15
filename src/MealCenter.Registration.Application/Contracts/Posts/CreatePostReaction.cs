using MealCenter.Registration.Domain.Posts;

namespace MealCenter.Registration.Application.Contracts.Posts
{
    public class CreatePostReaction
    {
        public Guid PostId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
