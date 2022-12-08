using MealCenter.Catalog.Domain;
using MealCenter.Catalog.Infrastructure.Context;
using MealCenter.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Catalog.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task<bool> CategoryAlreadyExists(string name)
        {
            return await _context.Categories.AsNoTracking().Where(c => c.Name == name).AnyAsync();
        }

        public async Task<bool> CategoryAlreadyExists(Guid id, string name)
        {
            return await _context.Categories.AsNoTracking().Where(c => c.Name == name && c.Id != id).AnyAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoryId(Guid id)
        {
            return await _context.Products.AsNoTracking().Where(p => p.CategoryId == id).ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<bool> ProductAlreadyExists(string name)
        {
            return await _context.Products.AsNoTracking().Where(p => p.Name == name).AnyAsync();
        }

        public async Task<bool> ProductAlreadyExists(Guid id, string name)
        {
            return await _context.Categories.AsNoTracking().Where(p => p.Name == name && p.Id != id).AnyAsync();

        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }

        public void RemoveCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
