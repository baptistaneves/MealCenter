using MealCenter.Core.Data;
using MealCenter.Orders.Domain;
using MealCenter.Orders.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MealCenter.Orders.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void AddMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder)
        {
            _context.MenuOptionsToOrder.Add(menuOptionToOrder);
        }

        
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MenuOptionToOrder>> GetAllMenuOptionToOrder()
        {
            return await _context.MenuOptionsToOrder.AsNoTracking().ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> GetDraftOrderByClientId(Guid clientId)
        {
            var order = await _context.Orders.AsNoTracking()
                .Where(o => o.ClientId == clientId && o.OrderStatus == OrderStatus.Draft).FirstOrDefaultAsync();

            if (order == null) return null;

            await _context.Entry(order).Collection(o => o.MenuOptionToOrders).LoadAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrderListByClientId(Guid clientId)
        {
            return await _context.Orders.AsNoTracking().Where(o => o.ClientId == clientId ).ToListAsync();
        }

        public async Task<MenuOptionToOrder> GetMenuOptionToOrderById(Guid id)
        {
            return await _context.MenuOptionsToOrder.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

        }

        public async Task<MenuOptionToOrder> GetMenuOptionToOrderByOrder(Guid orderId, Guid menuOptionId, Guid productId)
        {
            return await _context.MenuOptionsToOrder.AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == orderId && (o.MenuOptionId == menuOptionId || o.ProductId == productId));
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void RemoveMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder)
        {
            _context.MenuOptionsToOrder.Remove(menuOptionToOrder);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public void UpdateMenuOptionToOrder(MenuOptionToOrder menuOptionToOrder)
        {
            _context.MenuOptionsToOrder.Update(menuOptionToOrder);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
