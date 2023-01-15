using System.ComponentModel.DataAnnotations;

namespace MealCenter.Registration.Application.Contracts.Posts
{
    public class CreatePost
    {
        [Required(ErrorMessage = "Restaurant ID is required")]
        public Guid RestaurantId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
    }

    public class UpdatePost
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
