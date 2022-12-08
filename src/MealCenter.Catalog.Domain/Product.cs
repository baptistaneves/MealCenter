using MealCenter.Catalog.Domain.Validators;
using MealCenter.Core.DomainObjects;

namespace MealCenter.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }
        public string ImageUrl { get; private set; }

        //EF Rel.
        public Category Category { get; private set; }

        private Product() {}

        public void Active() => Status = true;

        public void Deactive() => Status = false;

        public void ChangeCategory(Category category)
        {
            Category = category;
            CategoryId = category.Id;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Description can not be null");

            Description = description;
        }

        public static Product CreateProduct(string name, string description, bool status, decimal price, Guid categoryId, string imageUrl)
        {
            var newProduct = new Product
            {
                Name = name,
                Description = description,
                Status = status,
                Price = price,
                CategoryId = categoryId,
                ImageUrl = imageUrl
            };

            return Validate(newProduct);
        }

        public void UpdateProduct(string name, string description, bool status, decimal price, Guid categoryId, string imageUrl)
        {
            Name = name;
            Description = description;
            Status = status;
            Price = price;
            CategoryId = categoryId;
            ImageUrl = imageUrl;

            Validate(this);
        }

        private static Product Validate(Product product)
        {
            var validator = new ProductValidator();

            var validationResult = validator.Validate(product);

            if (validationResult.IsValid) return product;

            var exception = new DomainException("Product error");

            validationResult.Errors.ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));

            throw exception;
        }
    }
}
