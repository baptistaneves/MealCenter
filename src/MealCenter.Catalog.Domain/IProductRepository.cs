using MealCenter.Core.Data;

namespace MealCenter.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        Task<Product> GetById(Guid id);
        Task<bool> ProductAlreadyExists(string name);
        Task<bool> ProductAlreadyExists(Guid id, string name);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAllProductsByCategoryId(Guid id);

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void RemoveCategory(Category category);
        Task<Category> GetCategoryById(Guid id);
        Task<bool> CategoryAlreadyExists(string name);
        Task<bool> CategoryAlreadyExists(Guid id, string name);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
