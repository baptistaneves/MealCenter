﻿using MealCenter.Core.Data;

namespace MealCenter.Orders.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Add(Order order); 
        void Update(Order order);  
        void Remove(Order order);
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetAll();

        void AddMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder);
        void UpdateMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder);
        void RemoveMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder);
        Task<MenuOptionToOrder> GetMenuOptionToOrderById(Guid id);
        Task<IEnumerable<MenuOptionToOrder>> GetAllMenuOptionToOrder();
    }
}
