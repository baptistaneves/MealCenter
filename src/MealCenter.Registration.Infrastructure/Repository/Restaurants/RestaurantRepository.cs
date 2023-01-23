using MealCenter.Core.Data;
using MealCenter.Registration.Domain.Restaurants;
using MealCenter.Registration.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Registration.Infrastructure.Repository.Restaurants
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RegistrationContext _context;

        public RestaurantRepository(RegistrationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Restaurant entity)
        {
            _context.Restaurants.Add(entity);
        }

        public void AddMenu(Menu menu)
        {
            _context.Menus.Add(menu);
        }

        public void AddMenuOption(MenuOption menuOption)
        {
            _context.MenuOptions.Add(menuOption);   
        }

        public void AddTable(Table table)
        {
            _context.Tables.Add(table);
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _context.Restaurants.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllMenu()
        {
            return await _context.Menus.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MenuOption>> GetAllMenuOption()
        {
            return await _context.MenuOptions.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAllTable()
        {
            return await _context.Tables.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAllFreeTable()
        {
            return await _context.Tables.AsNoTracking().Where(t => t.State == TableState.TableIsFree).ToListAsync();
        }

        public async Task<Restaurant> GetById(Guid id)
        {
            return await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Restaurant> GetRestaurantByIdentityId(string identityId)
        {
            return await _context.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.IdentityUserId == identityId);
        }

        public async Task<Menu> GetMenuById(Guid id)
        {
            return await _context.Menus.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task <MenuOption> GetMenuOptionById(Guid id)
        {
            return await _context.MenuOptions.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<MenuOption>> GetMenuOptionsByMenuId(Guid menuId)
        {
            return await _context.MenuOptions.AsNoTracking().Where(mo => mo.MenuId == menuId).ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetMenusByRestaurantId(Guid restaurantId)
        {
            return await _context.Menus.AsNoTracking().Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Table> GetTableById(Guid id)
        {
            return await _context.Tables.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Table> GetTableByClientId(Guid clientId)
        {
            return await _context.Tables.AsNoTracking().FirstOrDefaultAsync(r => r.ClientId == clientId);
        }

        public async Task<IEnumerable<Table>> GetTablesByRestaurantId(Guid restaurantId)
        {
            return await _context.Tables.AsNoTracking().Where(t => t.RestaurantId == restaurantId).ToListAsync();
        }

        public void Remove(Restaurant entity)
        {
            _context.Restaurants.Remove(entity);
        }

        public void RemoveMenu(Menu menu)
        {
            _context.Menus.Remove(menu);
        }

        public void RemoveMenuOption(MenuOption menuOption)
        {
            _context.MenuOptions.Remove(menuOption);
        }

        public void RemoveTable(Table table)
        {
            _context.Tables.Remove(table);
        }

        public void Update(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
        }

        public void UpdateMenu(Menu menu)
        {
            _context.Menus.Update(menu);
        }

        public void UpdateMenuOption(MenuOption menuOption)
        {
            _context.MenuOptions.Update(menuOption);
        }

        public void UpdateTable(Table table)
        {
            _context.Tables.Update(table);
        }

        public async Task<int> GetTheNumberOfRegisteredRestaurants()
        {
            return await _context.Restaurants.CountAsync();
        }

        public async Task<bool> RestaurantAlreadyExists(string name)
        {
            return await _context.Restaurants.AsNoTracking().Where(r => r.Name == name).AnyAsync();
        }

        public async Task<bool> RestaurantAlreadyExists(Guid id, string name)
        {
            return await _context.Restaurants.AsNoTracking().Where(r => r.Name == name && r.Id != id).AnyAsync();
        }

        public async Task<bool> TableAlreadyExists(int tableNumber)
        {
            return await _context.Tables.AsNoTracking().Where(t => t.TableNumber == tableNumber).AnyAsync();
        }

        public async Task<bool> TableAlreadyExists(Guid id, int tableNumber)
        {
            return await _context.Tables.AsNoTracking().Where(t => t.TableNumber == tableNumber && t.Id != id).AnyAsync();

        }

        public async Task<bool> MenuAlreadyExists(string type)
        {
            return await _context.Menus.AsNoTracking().Where(m => m.Type == type).AnyAsync();
        }

        public async Task<bool> MenuAlreadyExists(Guid id, string type)
        {
            return await _context.Menus.AsNoTracking().Where(m => m.Type == type && m.Id != id).AnyAsync();
        }

        public async Task<bool> MenuOptionAlreadyExists(string name)
        {
            return await _context.MenuOptions.AsNoTracking().Where(m => m.Name == name).AnyAsync();
        }

        public async Task<bool> MenuOptionAlreadyExists(Guid id, string name)
        {
            return await _context.MenuOptions.AsNoTracking().Where(m => m.Name == name && m.Id != id).AnyAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
