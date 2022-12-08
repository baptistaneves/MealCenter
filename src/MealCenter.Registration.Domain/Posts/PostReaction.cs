using MealCenter.Core.DomainObjects;

namespace MealCenter.Registration.Domain.Posts
{
    public class PostReaction : Entity
    {
        public Guid PostId { get; private set; }
        public string IdentityUserId { get; private set; }
        public ReactionType ReactionType { get; private set; }

        //EF Rel.
        public Post Post { get; private set; }

        public PostReaction(Guid postId, string identityUserId, ReactionType reactionType)
        {
            PostId = postId;
            IdentityUserId = identityUserId;
            ReactionType = reactionType;
        }
    }
}
