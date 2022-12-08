using FluentValidation;

namespace MealCenter.Catalog.Domain.Validators
{
    internal class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name must not be empty")
                .MinimumLength(4).WithMessage("Name must have at least 4 characters");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Category is required")
                .NotEqual(Guid.Empty).WithMessage("The provided category ID is not valid");

            RuleFor(p => p.ImageUrl)
                .NotEmpty().WithMessage("Image is required");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater then 0");
        }
    }
}
