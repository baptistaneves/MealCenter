using MealCenter.Core.DomainObjects;
using MealCenter.Registration.Domain.Restaurants;

namespace MealCenter.Registration.Domain.Posts
{
    public class Post : Entity, IAggregateRoot
    {
        public Guid RestaurantId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Restaurant Restaurant { get; private set; }
        private readonly List<PostComment> _postComments;
        private readonly List<PostReaction> _postReactions;

        public IReadOnlyCollection<PostComment> PostComments;
        public IReadOnlyCollection<PostReaction> PostReactions;


        public Post(Guid restaurantId, string title, string content, string imageUrl)
        {
            RestaurantId = restaurantId;
            Title = title;
            Content = content;
            ImageUrl = imageUrl;
        }

        public void UpdateImage(string newImageUrl)
        {
            ImageUrl = newImageUrl;
        }
        public void AddPostComment(PostComment newPostComment)
        {
            _postComments.Add(newPostComment);
        }

        public void RemovePostComment(PostComment postComment)
        {
            _postComments.Remove(postComment);
        }

        public void AddPostReaction(PostReaction newPostReaction)
        {
            _postReactions.Add(newPostReaction);
        }

        public void Remove(PostReaction postReaction)
        {
            _postReactions.Remove(postReaction);
        }
    }
}
