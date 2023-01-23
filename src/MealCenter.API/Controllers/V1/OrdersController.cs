namespace MealCenter.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
public class OrdersController : BaseController
{
    private readonly IOrderQueries _orderQueries;
    private readonly IProductAppService _productService;
    private readonly IRestaurantAppService _restaurantService;
    private readonly IMediatorHandler _mediatorHandler;
    public OrdersController(IMediatorHandler mediatorHandler,
                            INotificationHandler<DomainNotification> notifications,
                            IOrderQueries orderQueries,
                            IProductAppService productService,
                            IRestaurantAppService restaurantService) : base(mediatorHandler, notifications)
    {
        _orderQueries = orderQueries;
        _productService = productService;
        _restaurantService = restaurantService;
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet(ApiRoutes.Order.MyCart)]
    public async Task<ActionResult> MyCart()
    {
        return Ok(await _orderQueries.GetClientOrder(HttpContext.GetClientIdClaimValue()));
    }

    [HttpGet(ApiRoutes.Order.StartOrder)]
    public async Task<ActionResult> StartOrder()
    {
        var command = new StartOrderCommand(HttpContext.GetClientIdClaimValue());

        await _mediatorHandler.SendCommand(command);

        return Response();
    }


    [HttpGet(ApiRoutes.Order.CloseOrder)]
    public async Task<ActionResult> CloseOrder()
    {
        var command = new CloseOrderCommand(HttpContext.GetClientIdClaimValue());

        await _mediatorHandler.SendCommand(command);

        return Response();
    }

    [HttpPost(ApiRoutes.Order.MyCart)]
    [ValidateModel]
    public async Task<ActionResult> AddItemToCart([FromBody] MyCartViewModel myCartViewModel)
    {
        var product = await _productService.GetById(myCartViewModel.ProductId);
        var menuOption = await _restaurantService.GetMenuOptionById(myCartViewModel.MenuOptionId);

        var table = await _restaurantService.GetTableByClientId(HttpContext.GetClientIdClaimValue());
        if(table == null)
        {
            NotifyError("Order", "You must occupy a table");
            return Response();
        }

        var command = new AddMenuOptionToOrderCommand(HttpContext.GetIdentityIdClaimValue(), table.RestaurantId, table.Id, menuOption.Id,
            product.Id, menuOption.Name, product.Name, menuOption.Price, product.Price, myCartViewModel.ProductQuantity, myCartViewModel.MenuOptionQuantity);

        await _mediatorHandler.SendCommand(command);

        return Response();
    }

    [HttpDelete(ApiRoutes.Order.RemoveItem)]
    [ValidateModel]
    public async Task<ActionResult> RemoveItem(RemoveItemViewModel removeItemViewModel)
    {
        var command = new RemoveMenuOptionToOrderCommand(HttpContext.GetClientIdClaimValue(), removeItemViewModel.ProductId, removeItemViewModel.MenuOptionId);

        await _mediatorHandler.SendCommand(command);

        return Response();
    }

    [HttpPut(ApiRoutes.Order.UpdateItem)]
    public async Task<ActionResult> UpdateItem(UpdateItemViewModel updateItemViewModel)
    {
        var product = await _productService.GetById(updateItemViewModel.ProductId);
        var menuOption = await _restaurantService.GetMenuOptionById(updateItemViewModel.MenuOptionId);

        var command = new UpdateMenuOptionToOrderCommand(HttpContext.GetClientIdClaimValue(), 
            updateItemViewModel.ProductId, updateItemViewModel.MenuOptionId, updateItemViewModel.ProductQuantity, updateItemViewModel.MenuOptionQuantity);

        await _mediatorHandler.SendCommand(command);

        return Response();
    }


}

