using MealCenter.Core.DomainObjects;

namespace MealCenter.Identity.Domain
{
    public class UserProfile : Entity
    {
        public string IdentityId { get; private set; }
        public string Name { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        public UserProfile(string identityId, string name, string emailAddress, string phone)
        {
            IdentityId = identityId;
            Name = name;
            EmailAddress = emailAddress;
            Phone = phone;
        }

        protected UserProfile() { }
    }
}
