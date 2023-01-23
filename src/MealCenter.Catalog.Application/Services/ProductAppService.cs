using AutoMapper;
using MealCenter.Catalog.Application.Interfaces;
using MealCenter.Catalog.Application.ViewModels;
using MealCenter.Catalog.Domain;
using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;

namespace MealCenter.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;
        public ProductAppService(IProductRepository productRepository, IMediatorHandler mediatorHandler, IMapper mapper)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        public async Task Add(ProductViewModel product, CancellationToken cancellationToken)
        {
            if(await _productRepository.ProductAlreadyExists(product.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("product", "Provided product name already exists"));
                return;
            }
            
            var newProduct = Product.CreateProduct(product.Name, product.Description, product.Status, product.Price, product.CategoryId, product.ImageUrl);

            _productRepository.Add(newProduct);

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task AddCategory(CategoryViewModel productViewModel, CancellationToken cancellationToken)
        {
            if (await _productRepository.CategoryAlreadyExists(productViewModel.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Category", "Provided category name already exists"));
                return;
            }

            var category = Category.CreateCategory(productViewModel.Name);

            _productRepository.AddCategory(category);

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _productRepository.GetAllCategories();
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoryId(Guid id)
        {
            return await _productRepository.GetAllProductsByCategoryId(id);
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _productRepository.GetCategoryById(id);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task Remove(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(id);
            if(product is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Product", "Product with provided ID was not found"));
                return;
            }

            _productRepository.Remove(await _productRepository.GetById(id));
            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemoveCategory(Guid id, CancellationToken cancellationToken)
        {
            var category = await _productRepository.GetCategoryById(id);
            if (category is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Category", "Category with provided ID was not found"));
                return;
            }

            _productRepository.RemoveCategory(await _productRepository.GetCategoryById(id));
            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(Guid id, UpdateProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(id);

            if (product is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Product", "Product with provided ID was not found"));
                return;
            }

            if (await _productRepository.ProductAlreadyExists(id, productViewModel.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("product", "Provided product name already exists"));
                return;
            }

            product.UpdateProduct(productViewModel.Name, productViewModel.Description, productViewModel.Price);

            _productRepository.Update(product);

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateCategory(Guid id,CategoryViewModel categoryViewModel, CancellationToken cancellationToken)
        {
            var category = await _productRepository.GetCategoryById(id);
            if (category is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Category", "Category with provided ID was not found"));
                return;
            }

            if (await _productRepository.CategoryAlreadyExists(id, categoryViewModel.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("category", "Provided category name already exists"));
                return;
            }

            category.UpdateCategory(categoryViewModel.Name);

            _productRepository.UpdateCategory(category);

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

    }
}
