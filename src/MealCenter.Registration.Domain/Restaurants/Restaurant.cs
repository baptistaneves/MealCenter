using MealCenter.Core.DomainObjects;
using MealCenter.Registration.Domain.Posts;

namespace MealCenter.Registration.Domain.Restaurants
{
    public class Restaurant : Entity, IAggregateRoot
    {
        public string IdentityUserId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        private readonly List<Menu> _menus;
        private readonly List<Table> _tables;

        public IReadOnlyCollection<Menu> Menus;
        public IReadOnlyCollection<Menu> Tables;
        public IReadOnlyCollection<Post> Posts;

        public Restaurant(string identityUserId, string name, string location, string imageUrl, bool status, 
            string description)
        {
            IdentityUserId = identityUserId;
            Name = name;
            Location = location;
            ImageUrl = imageUrl;
            Status = status;
            Description = description;
        }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void UpdateImage(string newImageUrl)
        {
            ImageUrl = newImageUrl;
        }

        public void AddMenu(Menu newMenu)
        {
            _menus.Add(newMenu);
        }

        public void UpdateMenu(Menu menuUpdated)
        {
            menuUpdated.AssociateToRestaurant(Id);

            var currentMenu  = _menus.FirstOrDefault(m => m.Id == menuUpdated.Id);
            if(currentMenu != null)
            {
                _menus.Remove(currentMenu);
                _menus.Add(menuUpdated);
            }
        }

        public void RemoveMenu(Menu menu)
        {
            _menus.Remove(menu);
        }

        public void AddTable(Table newTable)
        {
            _tables.Add(newTable);
        }

        public void UpdateTable(Table tableUpdated)
        {
            tableUpdated.AssociateToRestaurant(Id);

            var currentTable = _tables.FirstOrDefault(m => m.Id == tableUpdated.Id);
            if (currentTable != null)
            {
                _tables.Remove(currentTable);
                _tables.Add(tableUpdated);
            }
        }

        public void RemoveTable(Table table)
        {
            _tables.Remove(table);
        }
    }
}
