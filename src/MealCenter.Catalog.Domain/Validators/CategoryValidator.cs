using FluentValidation;

namespace MealCenter.Catalog.Domain.Validators
{
    internal class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name must not be empty")
                .MinimumLength(4).WithMessage("Name must have at least 4 characters");
        }
    }
}
