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
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public RestaurantAppService(IMapper mapper, IRestaurantRepository restaurantRepository, IMediatorHandler meditorHandler)
        {
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _mediatorHandler = meditorHandler;
        }

        public async Task<Restaurant> Add(CreateRestaurant newRestaurant, string identityUserId, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateRestaurantValidator(), newRestaurant)) return null;

            if(await _restaurantRepository.RestaurantAlreadyExists(newRestaurant.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("restaurant", RestaurantErrorMessages.RestaurantNameAlreadyExists));
                return null;
            }

            var restaurant = new Restaurant(identityUserId, newRestaurant.Name, newRestaurant.EmailAddress, newRestaurant.Phone, newRestaurant.Location, newRestaurant.ImageUrl, newRestaurant.Status, newRestaurant.Description);
            _restaurantRepository.Add(restaurant);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
            return restaurant;
        }

        public async Task<Menu> AddMenu(CreateMenu newMenu, CancellationToken cancellationToken)
        {
            if (!Validate(new CreateMenuValidator(), newMenu)) return null;

            if(await _restaurantRepository.MenuAlreadyExists(newMenu.Type))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("menu", RestaurantErrorMessages.MenuTypeAlreadyExists));
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
                await _mediatorHandler.PublishNotification(new DomainNotification("menuOption", RestaurantErrorMessages.MenuOptionNameAlreadyExists));
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
                await _mediatorHandler.PublishNotification(new DomainNotification("table", RestaurantErrorMessages.TableNumberAlreadyExists));
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

        public async Task<Restaurant> GetRestaurantByIdentityId(string identityId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByIdentityId(identityId);
            if(restaurant == null)
            {
                _mediatorHandler.PublishNotification(new DomainNotification("Restaurant", "There is no Restaurant data for this identity"));
                return null;
            }

            return restaurant;
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

        public async Task<Table> GetTableByClientId(Guid clientId)
        {
            return await _restaurantRepository.GetTableByClientId(clientId);
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
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNotFound));
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
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNotFound));
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
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.RestaurantNotFound));
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
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.RestaurantNotFound));
                return;
            }

            restaurant.Deactivate();

            _restaurantRepository.Update(restaurant);
            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task Update(Guid id, UpdateRestaurant updatedRestaurant, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateRestaurantValidator(), updatedRestaurant)) return;

            var restaurant = await _restaurantRepository.GetById(id);

            if (restaurant == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Restaurant", RestaurantErrorMessages.RestaurantNotFound));
                return;
            }

            if (await _restaurantRepository.RestaurantAlreadyExists(id, restaurant.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Restaurant", RestaurantErrorMessages.RestaurantNameAlreadyExists));
                return;
            }

            restaurant.UpdateRestaurant(updatedRestaurant.Name, updatedRestaurant.Location, updatedRestaurant.Description, updatedRestaurant.Phone);
            
            _restaurantRepository.Update(restaurant);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateMenu(Guid id, UpdateMenu updatedMenu, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateMenuValidator(), updatedMenu)) return;

            var menu = await _restaurantRepository.GetMenuById(id);

            if (menu == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Menu", RestaurantErrorMessages.MenuNotFound));
                return;
            }

            if (await _restaurantRepository.MenuAlreadyExists(id, updatedMenu.Type))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Menu", RestaurantErrorMessages.MenuTypeAlreadyExists));
                return;
            }
            menu.UpdateMenu(updatedMenu.Type, updatedMenu.Status);

            _restaurantRepository.UpdateMenu(menu);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateMenuOption(Guid id, UpdateMenuOption updatedMenuOption, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateMenuOptionValidator(), updatedMenuOption)) return;

            var menuOption = await _restaurantRepository.GetMenuOptionById(id);

            if (menuOption == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("MenuOption", RestaurantErrorMessages.MenuOptionNotFound));
                return;
            }

            if (await _restaurantRepository.MenuOptionAlreadyExists(id, updatedMenuOption.Name))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("MenuOption", RestaurantErrorMessages.MenuOptionNameAlreadyExists));
                return;
            }

            menuOption.UpdateMenuOption(updatedMenuOption.MenuId, updatedMenuOption.Name, updatedMenuOption.Price, updatedMenuOption.Ingredients);

            _restaurantRepository.UpdateMenuOption(menuOption);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }

        public async Task UpdateTable(Guid id, UpdateTable updatedTable, CancellationToken cancellationToken)
        {
            if (!Validate(new UpdateTableValidator(), updatedTable)) return;

            var table = await _restaurantRepository.GetTableById(id);

            if (table == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNotFound));
                return;
            }

            if (await _restaurantRepository.TableAlreadyExists(id, updatedTable.TableNumber))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Table", RestaurantErrorMessages.TableNumberAlreadyExists));
                return;
            }

            table.UpdateTable(updatedTable.TableNumber);

            _restaurantRepository.UpdateTable(table);

            await _restaurantRepository.UnitOfWork.SaveAsync(cancellationToken);
        }


        public void Dispose()
        {
            _restaurantRepository?.Dispose();
        }
    }
}
