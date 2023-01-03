namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class LoginController : BaseController
    {
        private readonly JwtAdminService _jwtAdminService;
        private readonly IMediator _mediator;
        public LoginController(IMediatorHandler mediatorHandler,
                               INotificationHandler<DomainNotification> notifications,
                               JwtAdminService jwtAdminService,
                               IMediator mediator) : base(mediatorHandler, notifications)
        {
            _jwtAdminService = jwtAdminService;
            _mediator = mediator;
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
    }
}
