using MediatR;

namespace MealCenter.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            TimeStamp = DateTime.UtcNow;
            Key = key;
            Value = value;
        }
    }
}
