using MealCenter.Catalog.Application.ViewModels;
using MealCenter.Catalog.Domain;

namespace MealCenter.Catalog.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        Task Add(ProductViewModel product, CancellationToken cancellationToken);
        Task Update(Guid id, UpdateProductViewModel product, CancellationToken cancellationToken);
        Task Remove(Guid id, CancellationToken cancellationToken);
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAllProductsByCategoryId(Guid id);

        Task AddCategory(CategoryViewModel category, CancellationToken cancellationToken);
        Task UpdateCategory(Guid id, CategoryViewModel category, CancellationToken cancellationToken);
        Task RemoveCategory(Guid id, CancellationToken cancellationToken);
        Task<Category> GetCategoryById(Guid id);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
