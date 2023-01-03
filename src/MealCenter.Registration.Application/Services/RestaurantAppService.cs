using AutoMapper;
using MealCenter.Core.Communication.Mediator;
using MealCenter.Core.Messages.CommonMessages.Notifications;
using MealCenter.Registration.Application.Contracts.Restaurants;
using MealCenter.Registration.Application.ErrorMessages;
using MealCenter.Registration.Application.Interfaces;
using MealCenter.Registration.Application.Validators.Restaurants;
using MealCenter.Registration.Domain.Restaurants;

namespace MealCenter.Registration.Application.Services
{
    public class RestaurantAppService : BaseService, IRestaurantAppService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMediatorHandler _meditorHandler;
        private readonly IMapper _mapper;

        public RestaurantAppService(IMapper mapper, IRestaurantRepository restaurantRepository, IMediatorHandler meditorHandler)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _meditorHandler = meditorHandler;
        }

        public async Task<Restaurant> Add(CreateRestaurant newRestaurant, string identityUserId, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateRestaurantValidator(), newRestaurant)) return null;

            if(await _restaurantRepository.RestaurantAlreadyExists(newRestaurant.Name))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("restaurant", RestaurantErrorMessages.RestaurantNameAlreadyExists));
                return null;
            }

            var restaurant = new Restaurant(identityUserId, newRestaurant.Name, newRestaurant.Location, newRestaurant.ImageUrl, newRestaurant.Status, newRestaurant.Description);
            _restaurantRepository.Add(restaurant);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
            return restaurant;
        }

        public async Task<Menu> AddMenu(CreateMenu newMenu, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateMenuValidator(), newMenu)) return null;

            if(await _restaurantRepository.MenuAlreadyExists(newMenu.Type))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("menu", RestaurantErrorMessages.MenuTypeAlreadyExists));
                return null;
            }

            var menu = _mapper.Map<Menu>(newMenu);
            _restaurantRepository.AddMenu(menu);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
            return menu;
        }

        public async Task<MenuOption> AddMenuOption(CreateMenuOption newMenuOption, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateMenuOptionValidator(), newMenuOption)) return null;

            if (await _restaurantRepository.MenuAlreadyExists(newMenuOption.Name))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("menuOption", RestaurantErrorMessages.MenuOptionNameAlreadyExists));
                return null;
            }

            var menuOption = _mapper.Map<MenuOption>(newMenuOption);
            _restaurantRepository.AddMenuOption(menuOption);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
            return menuOption;
        }

        public async Task<Table> AddTable(CreateTable newTable, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateTableValidator(), newTable)) return null;

            if (await _restaurantRepository.TableAlreadyExists(newTable.TableNumber))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("table", RestaurantErrorMessages.TableNumberAlreadyExists));
                return null;
            }

            var table = _mapper.Map<Table>(newTable);
            _restaurantRepository.AddTable(table);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
            return table;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _restaurantRepository.GetAll();
        }

        public async Task<IEnumerable<Menu>> GetAllMenu()
        {
            return await _restaurantRepository.GetAllMenu();
        }

        public async Task<IEnumerable<MenuOption>> GetAllMenuOption()
        {
            return await _restaurantRepository.GetAllMenuOption();
        }

        public async Task<IEnumerable<Table>> GetAllTable()
        {
            return await _restaurantRepository.GetAllTable();
        }

        public async Task<Restaurant> GetById(Guid id)
        {
            return await _restaurantRepository.GetById(id);
        }

        public async Task<Menu> GetMenuById(Guid id)
        {
            return await _restaurantRepository.GetMenuById(id);
        }

        public async Task<MenuOption> GetMenuOptionById(Guid id)
        {
            return await _restaurantRepository.GetMenuOptionById(id);
        }

        public async Task<IEnumerable<MenuOption>> GetMenuOptionsByMenuId(Guid menuId)
        {
            return await _restaurantRepository.GetMenuOptionsByMenuId(menuId);
        }

        public async Task<IEnumerable<Menu>> GetMenusByRestaurantId(Guid restaurantId)
        {
            return await _restaurantRepository.GetMenusByRestaurantId(restaurantId);
        }

        public async Task<Table> GetTableById(Guid id)
        {
            return await _restaurantRepository.GetTableById(id);
        }

        public async Task<IEnumerable<Table>> GetTablesByRestaurantId(Guid restaurantId)
        {
            return await _restaurantRepository.GetTablesByRestaurantId(restaurantId);
        }

        public async Task Remove(Guid id, CancellationToken cancellationToken)
        {
            _restaurantRepository.Remove(await _restaurantRepository.GetById(id));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemoveMenu(Guid id, CancellationToken cancellationToken)
        {
            _restaurantRepository.RemoveMenu(await _restaurantRepository.GetMenuById(id));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemoveMenuOption(Guid id, CancellationToken cancellationToken)
        {
            _restaurantRepository.RemoveMenuOption(await _restaurantRepository.GetMenuOptionById(id));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task RemoveTable(Guid id, CancellationToken cancellationToken)
        {
            _restaurantRepository.RemoveTable(await _restaurantRepository.GetTableById(id));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task<int> GetTheNumberOfRegisteredRestaurants()
        {
            return await _restaurantRepository.GetTheNumberOfRegisteredRestaurants();
        }

        public async Task FreeTable(Guid id, CancellationToken cancellationToken)
        {
            var table = await _restaurantRepository.GetTableById(id);
            if(table == null)
            {
                await _meditorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNotFound));
                return;
            }

            table.FreeTable();

            _restaurantRepository.UpdateTable(table);
            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task OccupyTable(Guid id, Guid clientId, CancellationToken cancellationToken)
        {
            var table = await _restaurantRepository.GetTableById(id);
            if (table == null)
            {
                await _meditorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNotFound));
                return;
            }

            table.OccupyTable(clientId);

            _restaurantRepository.UpdateTable(table);
            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task ActivateRestaurant(Guid id, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            if (restaurant == null)
            {
                await _meditorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.RestaurantNotFound));
                return;
            }

            restaurant.Activate();

            _restaurantRepository.Update(restaurant);
            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task DeactivateRestaurant(Guid id, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.GetById(id);
            if (restaurant == null)
            {
                await _meditorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.RestaurantNotFound));
                return;
            }

            restaurant.Deactivate();

            _restaurantRepository.Update(restaurant);
            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(UpdateRestaurant restaurant, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateRestaurantValidator(), restaurant)) return;

            if (await _restaurantRepository.RestaurantAlreadyExists(restaurant.Id, restaurant.Name))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("restaurant", RestaurantErrorMessages.RestaurantNameAlreadyExists));
                return;
            }

            _restaurantRepository.Update(_mapper.Map<Restaurant>(restaurant));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateMenu(UpdateMenu menu, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateMenuValidator(), menu)) return;

            if (await _restaurantRepository.MenuAlreadyExists(menu.Id, menu.Type))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("menu", RestaurantErrorMessages.MenuTypeAlreadyExists));
                return;
            }

            _restaurantRepository.UpdateMenu(_mapper.Map<Menu>(menu));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateMenuOption(UpdateMenuOption menuOption, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateMenuOptionValidator(), menuOption)) return;

            if (await _restaurantRepository.MenuOptionAlreadyExists(menuOption.Id, menuOption.Name))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("menuOption", RestaurantErrorMessages.MenuOptionNameAlreadyExists));
                return;
            }

            _restaurantRepository.UpdateMenuOption(_mapper.Map<MenuOption>(menuOption));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateTable(UpdateTable table, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateTableValidator(), table)) return;

            if (await _restaurantRepository.TableAlreadyExists(table.Id, table.TableNumber))
            {
                await _meditorHandler.PublishNotification(new DomainNotification("table", RestaurantErrorMessages.TableNumberAlreadyExists));
                return;
            }

            _restaurantRepository.UpdateTable(_mapper.Map<Table>(table));

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }


        public void Dispose()
        {
            _restaurantRepository?.Dispose();
        }

    }
}
