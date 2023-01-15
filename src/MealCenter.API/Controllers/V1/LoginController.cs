namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class LoginController : BaseController
    {
        private readonly JwtAdminService _jwtAdminService;
        private readonly JwtClientService _jwtClientService;
        private readonly JwtRestaurantService _jwtRestaurantService;
        private readonly IClientAppService _clientAppService;
        private IRestaurantAppService _restaurantAppService;
        private readonly IMediator _mediator;

        public LoginController(IMediatorHandler mediatorHandler,
                               INotificationHandler<DomainNotification> notifications,
                               JwtAdminService jwtAdminService,
                               IMediator mediator,
                               IClientAppService clientAppService,
                               JwtClientService jwtClientService,
                               IRestaurantAppService restaurantAppService,
                               JwtRestaurantService jwtRestaurantService) : base(mediatorHandler, notifications)
        {
            _jwtAdminService = jwtAdminService;
            _mediator = mediator;
            _clientAppService = clientAppService;
            _jwtClientService = jwtClientService;
            _restaurantAppService = restaurantAppService;
            _jwtRestaurantService = jwtRestaurantService;
        }

        [HttpPost(ApiRoutes.Identity.LoginForAdminUser)]
        [ValidateModel]
        public async Task<ActionResult> LoginForUserAdmin([FromBody] LoginRequest login, CancellationToken cancellationToken)
        {
            var command = new LoginCommand { Email = login.Email, Password = login.Password };

            var identity = await _mediator.Send(command, cancellationToken);
            if (identity == null) return Response();

            var query = new GetAdminUserProfileByIdentityIdQuery { IdentityId = identity.Id };

            var userProfile = await _mediator.Send(query, cancellationToken);
            if (userProfile == null)
            {
                NotifyError("Identity", "There is no user profile for this identity");
                return Response();
            }

            string token = await _jwtAdminService.GetJwtString(identity, userProfile.Id);
            return Response(new
            {
                Email = userProfile.EmailAddress,
                Name = userProfile.Name,
                Phone = userProfile.Phone,
                Token = token
            });
        }

        [HttpPost(ApiRoutes.Identity.LoginForClient)]
        [ValidateModel]
        public async Task<ActionResult> LoginForClient([FromBody] LoginRequest login, CancellationToken cancellationToken)
        {
            var command = new LoginCommand { Email = login.Email, Password = login.Password };

            var identity = await _mediator.Send(command, cancellationToken);
            if (identity == null) return Response();

            var client = await _clientAppService.GetClientByIdentityId(identity.Id);
            if (client == null) return Response();

            var token = await _jwtClientService.GetJwtString(identity, client.Id);

            return Response(new
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                EmailAddress = client.EmailAddress,
                Phone = client.Phone,
                Token = token
            });
        }

        [HttpPost(ApiRoutes.Identity.LoginForRestaurant)]
        [ValidateModel]
        public async Task<ActionResult> LoginForRestaurant([FromBody] LoginRequest login, CancellationToken cancellationToken)
        {
            var command = new LoginCommand { Email = login.Email, Password = login.Password };

            var identity = await _mediator.Send(command, cancellationToken);
            if (identity == null) return Response();

            var restaurant = await _restaurantAppService.GetRestaurantByIdentityId(identity.Id);
            if(restaurant == null) return Response();

            var token = await _jwtRestaurantService.GetJwtString(identity, restaurant.Id);

            return Response(new
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                EmailAddress = restaurant.EmailAddress,
                Phone = restaurant.Phone,
                Token = token
            });
        }
    }
}
