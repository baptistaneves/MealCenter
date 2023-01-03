namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class ClientsController : BaseController
    {
        private readonly IClientAppService _clientAppService;
        private readonly JwtClientService _jwtClientService;
        private readonly IMediator _mediator;
        public ClientsController(IMediatorHandler mediatorHandler,
                                 INotificationHandler<DomainNotification> notifications,
                                 IClientAppService clientAppService,
                                 IMediator mediator,
                                 JwtClientService jwtClientService) : base(mediatorHandler, notifications)
        {
            _clientAppService = clientAppService;
            _mediator = mediator;
            _jwtClientService = jwtClientService;
        }

        [HttpGet]
        public async Task<ActionResult>GetAllClient()
        {
            return Ok(await _clientAppService.GetAll());
        }

        [HttpGet(ApiRoutes.Client.GetClientById)]
        [ValidateGuid("id")]
        public async Task<ActionResult> GetClientById(Guid id)
        {
            var client = await _clientAppService.GetById(id);
            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpGet(ApiRoutes.Client.GetTheNumberOfRegisteredClients)]
        public async Task<int> GetTheNumberOfRegisteredClients()
        {
            return await _clientAppService.GetTheNumberOfRegisteredClients();
        }

        [HttpGet(ApiRoutes.Client.ActivateClient)]
        [ValidateGuid("id")]
        public async Task<ActionResult> ActivateClient(Guid id, CancellationToken cancellationToken)
        {
            await _clientAppService.ActivateClient(id, cancellationToken);
            return Response();
        }

        [HttpGet(ApiRoutes.Client.DeactivateClient)]
        [ValidateGuid("id")]
        public async Task<ActionResult> DeactivateClient(Guid id, CancellationToken cancellationToken)
        {
            await _clientAppService.DeactivateClient(id, cancellationToken);
            return Response();
        }

        [HttpPost(ApiRoutes.Client.AddClient)]
        [ValidateModel]
        public async Task<ActionResult> AddClient([FromBody] CreateClient createClient, CancellationToken cancellationToken)
        {
            var command  = new RegisterClientIdentityUserCommand { Email = createClient.EmailAddress, Password = createClient.Password };
            var newIdentity = await _mediator.Send(command, cancellationToken);

            if (newIdentity == null) return Response(createClient);

            var newClient =  await _clientAppService.Add(createClient, newIdentity.Id, cancellationToken);

            var token = await _jwtClientService.GetJwtString(newIdentity, newClient.Id);

            return Response(new
            {
                FirstName = newClient.FirstName,
                LastName = newClient.LastName,
                EmailAddress = newClient.EmailAddress,
                Phone = newClient.Phone,
                Token = token
            });
        }

        [HttpPut(ApiRoutes.Client.UpdateClient)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<ActionResult> UpdateClient(Guid id, [FromBody] UpdateClient updateClient, CancellationToken cancellationToken)
        {
            await _clientAppService.Update(id, updateClient, cancellationToken);
            return Response();
        }

        [HttpDelete(ApiRoutes.Client.RemoveClient)]
        [ValidateGuid("id")]
        public async Task<ActionResult> RemoveClient(Guid id, CancellationToken cancellationToken)
        {
            var client =  await _clientAppService.GetById(id);
            if (client == null) return NotFound();

            await _clientAppService.Remove(client, cancellationToken);

            return Ok();
        }
    }
}
