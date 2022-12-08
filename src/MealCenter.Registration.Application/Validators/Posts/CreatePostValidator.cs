using FluentValidation;
using MealCenter.Registration.Application.Contracts.Posts;

namespace MealCenter.Registration.Application.Validators.Posts
{
    internal class CreatePostValidator : AbstractValidator<CreatePost>
    {
        public CreatePostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("Content is required");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("Image is required to create a post");

            RuleFor(p => p.RestaurantId)
                .NotEmpty().WithMessage("Restaurant is required")
                .NotEqual(Guid.Empty).WithMessage("Restaurant ID is not valid");
        }
    }

    internal class UpdatePostValidator : AbstractValidator<UpdatePost>
    {
        public UpdatePostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage("Post content should not be empty");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("Image is required to create a post");
        }
    }

}
