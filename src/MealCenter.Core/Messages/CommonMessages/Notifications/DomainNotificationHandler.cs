using MediatR;

namespace MealCenter.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public async Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
           _notifications.Add(notification);
        }

        public virtual List<DomainNotification> GetNotification()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}
