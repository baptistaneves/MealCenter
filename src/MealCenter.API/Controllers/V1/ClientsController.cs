namespace MealCenter.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class ClientsController : BaseController
    {
        public ClientsController(IMediatorHandler mediatorHandler, 
                                 INotificationHandler<DomainNotification> notifications) : base(mediatorHandler, notifications)
        {
        }
    }
}
