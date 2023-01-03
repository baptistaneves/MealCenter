namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class UserProfilesController : BaseController
    {
        private readonly IMediator _mediator;
        public UserProfilesController(IMediatorHandler mediatorHandler,
                                      INotificationHandler<DomainNotification> notifications,
                                      IMediator mediator) : base(mediatorHandler, notifications)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAdminUserProfiles(CancellationToken token)
        {
            var query = new GetAllAdminUserProfilesQuery();

            var result = await _mediator.Send(query, token);

            return Response(result);
        }

        [HttpGet(ApiRoutes.Identity.GetAdminUserProfileById)]
        [ValidateGuid("{id}")]
        public async Task<ActionResult> GetAdminUserProfileById(string id, CancellationToken token)
        {
            var query = new GetAdminUserProfileByIdentityIdQuery { IdentityId = id };

            var result =  await _mediator.Send(query, token);

            if (result == null) return NotFound();

            return Response(result);
        }

        [HttpPost(ApiRoutes.Identity.CreateAdminUserProfileIdentity)]
        [ValidateModel]
        public async Task<ActionResult> CreateAdminUserProfileIdentity(CreateUserProfileIdentity newUserProfile, CancellationToken token)
        {
            var command = new RegisterAdminIdentityUserCommand
            {
                Name= newUserProfile.Name,
                EmailAddress= newUserProfile.EmailAddress,
                Password = newUserProfile.Password,
                Phone = newUserProfile.Phone
            };

            var result = await _mediator.Send(command, token);

            return Response(result);
        }
        
    }
}