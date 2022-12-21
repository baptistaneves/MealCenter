using MealCenter.Core.Data;

namespace MealCenter.Registration.Domain.Restaurants
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetAll();
        Task<Restaurant> GetById(Guid id);
        Task<int> GetTheNumberOfRegisteredRestaurants();
        Task<bool> RestaurantAlreadyExists(string name);
        Task<bool> RestaurantAlreadyExists(Guid id, string name);
        void Add(Restaurant entity);
        void Update(Restaurant entity);
        void Remove(Restaurant entity);

        Task<Table> GetTableById(Guid id);
        Task<IEnumerable<Table>> GetAllTable();
        Task<IEnumerable<Table>> GetAllFreeTable();
        Task<IEnumerable<Table>> GetTablesByRestaurantId(Guid restaurantId);
        Task<bool> TableAlreadyExists(int tableNumber);
        Task<bool> TableAlreadyExists(Guid id, int tableNumber);
        void AddTable(Table table);
        void UpdateTable(Table table);
        void RemoveTable(Table table);

        Task<Menu> GetMenuById(Guid id);
        Task<IEnumerable<Menu>> GetAllMenu();
        Task<bool> MenuAlreadyExists(string type);
        Task<bool> MenuAlreadyExists(Guid id, string type);
        Task<IEnumerable<Menu>> GetMenusByRestaurantId(Guid restaurantId);
        void AddMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void RemoveMenu(Menu menu);

        Task<MenuOption> GetMenuOptionById(Guid id);
        Task<IEnumerable<MenuOption>> GetAllMenuOption();
        Task<IEnumerable<MenuOption>> GetMenuOptionsByMenuId(Guid menuId);
        Task<bool> MenuOptionAlreadyExists(string name);
        Task<bool> MenuOptionAlreadyExists(Guid id, string name);
        void AddMenuOption(MenuOption menuOption);
        void UpdateMenuOption(MenuOption menuOption);
        void RemoveMenuOption(MenuOption menuOption);
    }
}
