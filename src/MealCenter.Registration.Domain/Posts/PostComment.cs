using MealCenter.Core.DomainObjects;

namespace MealCenter.Registration.Domain.Posts
{
    public class PostComment : Entity
    {
        public Guid PostId { get; private set; }
        public string IdentityUserId { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Post Post { get; set; }


        public PostComment(Guid postId, string identityUserId, string comment)
        {
            PostId = postId;
            IdentityUserId = identityUserId;
            Comment = comment;
        }

    }
}
