namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class RestaurantsController : BaseController
    {
        private readonly IRestaurantAppService _restaurantService;
        private readonly JwtRestaurantService _jwtRestaurant;
        private readonly IMediator _mediator;
        public RestaurantsController(IMediatorHandler mediatorHandler,
                                     INotificationHandler<DomainNotification> notifications,
                                     IRestaurantAppService restaurantService,
                                     IMediator mediator,
                                     JwtRestaurantService jwtRestaurant) : base(mediatorHandler, notifications)
        {
            _restaurantService = restaurantService;
            _mediator = mediator;
            _jwtRestaurant = jwtRestaurant;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRestaurant()
        {
            return Ok(await _restaurantService.GetAll());
        }

        [HttpGet(ApiRoutes.Restaurant.GetAllMenu)]
        public async Task<ActionResult> GetAllMenu()
        {
            return Ok(await _restaurantService.GetAllMenu());
        }

        [HttpGet(ApiRoutes.Restaurant.GetAllMenuOption)]
        public async Task<ActionResult> GetAllMenuOption()
        {
            return Ok(await _restaurantService.GetAllMenuOption());
        }

        [HttpGet(ApiRoutes.Restaurant.GetAllTable)]
        public async Task<ActionResult> GetAllTable()
        {
            return Ok(await _restaurantService.GetAllTable());
        }

        [HttpGet(ApiRoutes.Restaurant.GetById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetRestaurantById(Guid id)
        {
            return Ok(await _restaurantService.GetById(id));
        }

        [HttpGet(ApiRoutes.Restaurant.GetMenuById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetMenuById(Guid id)
        {
            return Ok(await _restaurantService.GetMenuById(id));
        }

        [HttpGet(ApiRoutes.Restaurant.GetMenuOptionById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetMenuOptionById(Guid id)
        {
            return Ok(await _restaurantService.GetMenuOptionById(id));
        }

        [HttpGet(ApiRoutes.Restaurant.GetTableById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetTableById(Guid id)
        {
            return Ok(await _restaurantService.GetTableById(id));
        }

        [HttpGet(ApiRoutes.Restaurant.GetMenuOptionsByMenuId)]
        [ValidateGuid("menuId")]
        public async Task<ActionResult> GetMenuOptionsByMenuId(Guid menuId)
        {
            return Ok(await _restaurantService.GetMenuOptionsByMenuId(menuId));
        }

        [HttpGet(ApiRoutes.Restaurant.GetMenusByRestaurantId)]
        [ValidateGuid("restaurantId")]
        public async Task<ActionResult> GetMenusByRestaurantId(Guid restaurantId)
        {
            return Ok(await _restaurantService.GetMenusByRestaurantId(restaurantId));
        }

        [HttpGet(ApiRoutes.Restaurant.GetTablesByRestaurantId)]
        [ValidateGuid("restaurantId")]
        public async Task<ActionResult> GetTablesByRestaurantId(Guid restaurantId)
        {
            return Ok(await _restaurantService.GetTablesByRestaurantId(restaurantId));
        }

        [HttpGet(ApiRoutes.Restaurant.GetTheNumberOfRegisteredRestaurants)]
        public async Task<ActionResult> GetTheNumberOfRegisteredRestaurants()
        {
            return Ok(await _restaurantService.GetTheNumberOfRegisteredRestaurants());
        }

        [HttpGet(ApiRoutes.Restaurant.FreeTable)]
        [ValidateGuid("id")]
        public async Task<ActionResult> FreeTable(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.FreeTable(id, cancellationToken);
            return Ok();
        }

        [HttpGet(ApiRoutes.Restaurant.OccupyTable)]
        [ValidateGuid("id", "clientId")]
        public async Task<ActionResult> OccupyTable(Guid id, Guid clientId, CancellationToken cancellationToken)
        {
            await _restaurantService.OccupyTable(id, clientId, cancellationToken);
            return Ok();
        }

        [HttpGet(ApiRoutes.Restaurant.ActivateRestaurant)]
        [ValidateGuid("id")]
        public async Task<ActionResult> ActivateRestaurant(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.ActivateRestaurant(id, cancellationToken);
            return Ok();
        }

        [HttpGet(ApiRoutes.Restaurant.DeactivateRestaurant)]
        [ValidateGuid("id")]
        public async Task<ActionResult> DeactivateRestaurant(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.DeactivateRestaurant(id, cancellationToken);
            return Ok();
        }

        [HttpPost(ApiRoutes.Restaurant.AddRestaurant)]
        [ValidateModel]
        public async Task<ActionResult> AddRestaurant([FromBody] CreateRestaurant createRestaurant, CancellationToken cancellationToken)
        {
            var command = new RegisterRestaurantIdentityUserCommand { Email = createRestaurant.EmailAddress, Password = createRestaurant.Password };
            var newIdentity = await _mediator.Send(command, cancellationToken);

            if (newIdentity == null) return Response(createRestaurant);

            var newRestaurant = await _restaurantService.Add(createRestaurant, newIdentity.Id, cancellationToken);

            var token = await _jwtRestaurant.GetJwtString(newIdentity, newRestaurant.Id);

            return Response(new
            {
                Id = newRestaurant.Id,
                Name = newRestaurant.Name,
                Location = newRestaurant.Location,
                EmailAddress = newRestaurant.EmailAddress,
                Phone = newRestaurant.Phone,
                Token = token
            });
        }

        [HttpPost(ApiRoutes.Restaurant.AddMenu)]
        [ValidateModel]
        public async Task<ActionResult> AddMenu([FromBody] CreateMenu createMenu, CancellationToken cancellationToken)
        {
            var newMenu = await _restaurantService.AddMenu(createMenu, cancellationToken);
            return Response(newMenu);
        }

        [HttpPost(ApiRoutes.Restaurant.AddMenuOption)]
        [ValidateModel]
        public async Task<ActionResult> AddMenuOption([FromBody] CreateMenuOption createMenuOption, CancellationToken cancellationToken)
        {
            var newMenuOption = await _restaurantService.AddMenuOption(createMenuOption, cancellationToken);
            return Response(newMenuOption);
        }

        [HttpPost(ApiRoutes.Restaurant.AddTable)]
        [ValidateModel]
        public async Task<ActionResult> AddTable([FromBody] CreateTable createTable, CancellationToken cancellationToken)
        {
            var newTable = await _restaurantService.AddTable(createTable, cancellationToken);
            return Response(newTable);
        }


        [HttpPut(ApiRoutes.Restaurant.UpdateRestaurant)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurant updateRestaurant, CancellationToken cancellationToken)
        {
            updateRestaurant.Id = id;
            await _restaurantService.Update(updateRestaurant, cancellationToken);

            return Response();
        }

        [HttpPut(ApiRoutes.Restaurant.UpdateMenu)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdateMenu(Guid id, [FromBody] UpdateMenu updateMenu, CancellationToken cancellationToken)
        {
            updateMenu.Id = id;
            await _restaurantService.UpdateMenu(updateMenu, cancellationToken);

            return Response();
        }

        [HttpPut(ApiRoutes.Restaurant.UpdateMenuOption)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdateMenuOption(Guid id, [FromBody] UpdateMenuOption updateMenuOption, CancellationToken cancellationToken)
        {
            updateMenuOption.Id = id;
            await _restaurantService.UpdateMenuOption(updateMenuOption, cancellationToken);

            return Response();
        }

        [HttpPut(ApiRoutes.Restaurant.UpdateTable)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdateTable(Guid id, [FromBody] UpdateTable updateTable, CancellationToken cancellationToken)
        {
            updateTable.Id = id;
            await _restaurantService.UpdateTable(updateTable, cancellationToken);

            return Response();
        }

        [HttpDelete(ApiRoutes.Restaurant.RemoveRestaurant)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveRestaurant(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.Remove(id, cancellationToken);
            return Ok();
        }

        [HttpDelete(ApiRoutes.Restaurant.RemoveMenu)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveMenu(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.RemoveMenu(id, cancellationToken);
            return Ok();
        }

        [HttpDelete(ApiRoutes.Restaurant.RemoveMenuOption)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveMenuOption(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.RemoveMenuOption(id, cancellationToken);
            return Ok();
        }

        [HttpDelete(ApiRoutes.Restaurant.RemoveTable)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveTable(Guid id, CancellationToken cancellationToken)
        {
            await _restaurantService.RemoveTable(id, cancellationToken);
            return Ok();
        }
    }
}
