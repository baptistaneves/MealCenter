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

            _productRepository.Add(_mapper.Map<Product>(product));

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task AddCategory(CategoryViewModel category, CancellationToken cancellationToken)
        {
            if (await _productRepository.CategoryAlreadyExists(category.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("category", "Provided category name already exists"));
                return;
            }

            _productRepository.AddCategory(_mapper.Map<Category>(category));

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
            _productRepository.Remove(await _productRepository.GetById(id));
            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemoveCategory(Guid id, CancellationToken cancellationToken)
        {
            _productRepository.RemoveCategory(await _productRepository.GetCategoryById(id));
            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(ProductViewModel product, CancellationToken cancellationToken)
        {
            if (await _productRepository.ProductAlreadyExists(product.Id, product.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("product", "Provided product name already exists"));
                return;
            }

            _productRepository.Update(_mapper.Map<Product>(product));

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateCategory(CategoryViewModel category, CancellationToken cancellationToken)
        {
            if (await _productRepository.CategoryAlreadyExists(category.Id, category.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("category", "Provided category name already exists"));
                return;
            }

            _productRepository.UpdateCategory(_mapper.Map<Category>(category));

            await _productRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

    }
}
