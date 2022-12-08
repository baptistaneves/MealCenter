using FluentValidation;
using MealCenter.Registration.Application.Contracts.Posts;

namespace MealCenter.Registration.Application.Validators.Posts
{
    internal class CreatePostCommentValidator : AbstractValidator<CreatePostComment>
    {
        public CreatePostCommentValidator()
        {
            RuleFor(p => p.Comment)
                .NotEmpty().WithMessage("Comment text should not be empty")
                .MinimumLength(1).WithMessage("Comment should have at least 1 character");

            RuleFor(p => p.PostId)
                .NotEmpty().WithMessage("Post is required")
                .NotEqual(Guid.Empty).WithMessage("Post ID is not valid");
        }
    }

    internal class UpdatePostCommentValidator : AbstractValidator<UpdatePostComment>
    {
        public UpdatePostCommentValidator()
        {
            RuleFor(p => p.Comment)
                .NotEmpty().WithMessage("Comment text should not be empty")
                .MinimumLength(1).WithMessage("Comment should have at least 1 character");
        }
    }
}
