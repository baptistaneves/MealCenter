namespace MealCenter.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly DomainNotificationHandler _notifications;

        public BaseController(IMediatorHandler mediatorHandler,
                              INotificationHandler<DomainNotification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
        }

        [NonAction]
        protected ActionResult Response(object result = null)
        {
            if (OperationIsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = GetMessageErrors()
            });
        }

        [NonAction]
        protected ActionResult Response(ModelStateDictionary modelSate)
        {
            if (!modelSate.IsValid) NotifyModelStateError(modelSate);

            return Response();
        }

        [NonAction]
        protected void NotifyModelStateError(ModelStateDictionary modelSate)
        {
            var errors = modelSate.Values.SelectMany(m => m.Errors);

            foreach (var error in errors)
            {
                NotifyError("ModelState", error.ErrorMessage);
            }
        }

        [NonAction]
        protected bool OperationIsValid()
        {
            return !_notifications.HasNotifications();
        }

        [NonAction]
        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message)); 
        }

        [NonAction]
        private IEnumerable<string> GetMessageErrors()
        {
            return _notifications.GetNotifications().Select(m => m.Value).ToList();
        }
    }
}
