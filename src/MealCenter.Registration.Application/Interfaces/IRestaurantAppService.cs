using MealCenter.Registration.Application.Contracts.Restaurants;
using MealCenter.Registration.Domain.Restaurants;

namespace MealCenter.Registration.Application.Interfaces
{
    public interface IRestaurantAppService : IDisposable
    {
        Task<IEnumerable<Restaurant>> GetAll();
        Task<Restaurant> GetById(Guid id);
        Task<int> GetTheNumberOfRegisteredRestaurants();
        Task<Restaurant> Add(CreateRestaurant newRestaurant, string identityUserId, CancellationToken cancellationToken);
        Task Update(UpdateRestaurant restaurant, CancellationToken cancellationToken);
        Task ActivateRestaurant(Guid id, CancellationToken cancellationToken);
        Task DeactivateRestaurant(Guid id, CancellationToken cancellationToken);
        Task Remove(Guid id, CancellationToken cancellationToken);

        Task<Table> GetTableById(Guid id);
        Task<IEnumerable<Table>> GetAllTable();
        Task<IEnumerable<Table>> GetTablesByRestaurantId(Guid restaurantId);
        Task<Table> AddTable(CreateTable newTable, CancellationToken cancellationToken);
        Task UpdateTable(UpdateTable table, CancellationToken cancellationToken);
        Task RemoveTable(Guid id, CancellationToken cancellationToken);
        Task FreeTable(Guid id, CancellationToken cancellationToken);
        Task OccupyTable(Guid id, Guid clientId, CancellationToken cancellationToken);

        Task<Menu> GetMenuById(Guid id);
        Task<IEnumerable<Menu>> GetAllMenu();
        Task<IEnumerable<Menu>> GetMenusByRestaurantId(Guid restaurantId);
        Task<Menu> AddMenu(CreateMenu newMenu, CancellationToken cancellationToken);
        Task UpdateMenu(UpdateMenu menu, CancellationToken cancellationToken);
        Task RemoveMenu(Guid id, CancellationToken cancellationToken);

        Task<MenuOption> GetMenuOptionById(Guid id);
        Task<IEnumerable<MenuOption>> GetAllMenuOption();
        Task<IEnumerable<MenuOption>> GetMenuOptionsByMenuId(Guid menuId);
        Task<MenuOption> AddMenuOption(CreateMenuOption newMenuOption, CancellationToken cancellationToken);
        Task UpdateMenuOption(UpdateMenuOption menuOption, CancellationToken cancellationToken);
        Task RemoveMenuOption(Guid id, CancellationToken cancellationToken);
    }
}
