namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class ProductsController : BaseController
    {
        private readonly IProductAppService _produtoService;
        public ProductsController(IMediatorHandler mediatorHandler,
                                  INotificationHandler<DomainNotification> notifications,
                                  IProductAppService produtoService) : base(mediatorHandler, notifications)
        {
            _produtoService = produtoService;
        }

        [HttpGet(ApiRoutes.Product.GetAllProducts)]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet(ApiRoutes.Product.GetAllProductsByCategoryId)]
        [ValidateGuid("categoryId")]
        public async Task<ActionResult> GetAllProductsByCategoryId(Guid categoryId)
        {
            return Ok(await _produtoService.GetAllProductsByCategoryId(categoryId));
        }

        [HttpGet(ApiRoutes.Product.GetAllCategories)]
        public async Task<ActionResult> GetAllCategories()
        {
            return Ok(await _produtoService.GetAllCategories());
        }

        [HttpGet(ApiRoutes.Product.GetProductById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            var product = await _produtoService.GetById(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet(ApiRoutes.Product.GetCategoryById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetCategoryById(Guid id)
        {
            var category = await _produtoService.GetCategoryById(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpDelete(ApiRoutes.Product.RemoveProduct)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveProduct(Guid id, CancellationToken cancellationToken)
        {
            await _produtoService.Remove(id, cancellationToken);
            return Response();
        }

        [HttpDelete(ApiRoutes.Product.RemoveCategory)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveCategory(Guid id, CancellationToken cancellationToken)
        {
            await _produtoService.RemoveCategory(id, cancellationToken);
            return Response();
        }

        [HttpPost(ApiRoutes.Product.AddProduct)]
        [ValidateModel]
        public async Task<ActionResult> AddProduct(ProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            await _produtoService.Add(productViewModel, cancellationToken);

            return Response(productViewModel);
        }

        [HttpPost(ApiRoutes.Product.AddCategory)]
        [ValidateModel]
        public async Task<ActionResult> AddCategory(CategoryViewModel categoryViewModel, CancellationToken cancellationToken)
        {
            await _produtoService.AddCategory(categoryViewModel, cancellationToken);

            return Response(categoryViewModel);
        }

        [HttpPut(ApiRoutes.Product.UpdateProduct)]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<ActionResult> UpdateProduct(Guid id, UpdateProductViewModel productViewModel, CancellationToken cancellationToken)
        {
            await _produtoService.Update(id, productViewModel, cancellationToken);

            return Response(productViewModel);
        }

        [HttpPut(ApiRoutes.Product.UpdateCategory)]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<ActionResult> UpdateCategory(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken)
        {
            await _produtoService.UpdateCategory(id, categoryViewModel, cancellationToken);

            return Response(categoryViewModel);
        }
    }
}
