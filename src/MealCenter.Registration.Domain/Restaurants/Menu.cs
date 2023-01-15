using MealCenter.Core.DomainObjects;

namespace MealCenter.Registration.Domain.Restaurants
{
    public class Menu : Entity
    {
        public Guid RestaurantId { get; private set; }
        public bool Status { get; private set; }
        public string Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Restaurant Restaurant { get; private set; }
        private readonly List<MenuOption> _menuOptions;

        public IReadOnlyCollection<MenuOption> MenuOptions;

        public Menu(Guid restaurantId, string type, bool status)
        {
            RestaurantId = restaurantId;
            Type = type;
            Status = status;
        }

        //EF
        protected Menu() { }

        public void AssociateToRestaurant(Guid restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void AddMenuOption(MenuOption newMenuOption)
        {
            _menuOptions.Add(newMenuOption);
        }

        public void UpdateMenu(string type, bool status)
        {
            Type = type;
            Status = status;
        }

        public void UpdateMenuOption(MenuOption menuOptionUpdated)
        {
            menuOptionUpdated.AssociateToMenu(Id);

            var currentMenuOption = _menuOptions.FirstOrDefault(m=> m.Id == menuOptionUpdated.Id);
            if(currentMenuOption != null)
            {
                _menuOptions.Remove(currentMenuOption);
                _menuOptions.Add(menuOptionUpdated);
            }
        }

        public void RemoveMenuOption(MenuOption menuOption)
        {
            _menuOptions.Remove(menuOption);
        }

    }
}
