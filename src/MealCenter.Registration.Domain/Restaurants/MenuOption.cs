using MealCenter.Core.DomainObjects;

namespace MealCenter.Registration.Domain.Restaurants
{
    public class MenuOption : Entity
    {
        public Guid MenuId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool Status { get; private set; }
        public string Ingredients { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; private set; }

        //EF Rel.
        public Menu Menu { get; private set; }

        public MenuOption(Guid menuId, string name, decimal price, string ingredients, bool status)
        {
            MenuId = menuId;
            Name = name;
            Price = price;
            Ingredients = ingredients;
            Status = status;
        }

        public void Activate() => Status = true;

        public void Deactivate() => Status = false;

        public void AssociateToMenu(Guid menuId)
        {
            MenuId = menuId;
        }

    }
}
