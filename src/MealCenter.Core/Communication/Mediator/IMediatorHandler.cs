using MealCenter.Core.Messages;
using MealCenter.Core.Messages.CommonMessages.Notifications;

namespace MealCenter.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
    }
}
