using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Posts
{
    public class CreatePostComment
    {
        [Required(ErrorMessage = "Post ID is required")]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }

    public class UpdatePostComment
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
    }
}
