using MealCenter.Catalog.Domain.Validators;
using MealCenter.Core.DomainObjects;

namespace MealCenter.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        public ICollection<Product> Products { get; private set; }

        private Category() { }

        public static Category CreateCategory(string name)
        {
            var newCategory = new Category
            {
                Name = name
            };

            return Validate(newCategory);
        }

        public void UpdateCategory(string name)
        {
            Name = name;

            Validate(this);
        }

        private static Category Validate(Category category)
        {
            var validator = new CategoryValidator();

            var validationResult = validator.Validate(category);

            if (validationResult.IsValid) return category;

            var exception = new DomainException("Category error");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }
    }
}
