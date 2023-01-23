using MealCenter.Orders.Application.Queries.ViewModels;
using MealCenter.Orders.Domain;

namespace MealCenter.Orders.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ClientOrderViewModel> GetClientOrder(Guid clientId)
        {
            var order = await _orderRepository.GetDraftOrderByClientId(clientId);
            if (clientId == null) return null;

            var clientOrder = new ClientOrderViewModel
            {
                ClientId = order.ClientId,   
                TableId = order.TableId,
                RestaurantId = order.RestaurantId,
                OrderId = order.Id,
                Amount = order.Amount
            };

            foreach (var menuOptionToOrder in clientOrder.MenuOptionsToOrder)
            {
                clientOrder.MenuOptionsToOrder.Add(new MenuOptionToOrderViewModel
                {
                    OrderId = menuOptionToOrder.OrderId,
                    ProductId = menuOptionToOrder.ProductId,
                    MenuOptionId = menuOptionToOrder.MenuOptionId,
                    MenuOptionName = menuOptionToOrder.MenuOptionName,
                    MenuOptionPrice = menuOptionToOrder.MenuOptionPrice,
                    MenuOptionQuantity = menuOptionToOrder.MenuOptionQuantity,
                    ProductName = menuOptionToOrder.ProductName,
                    ProductPrice = menuOptionToOrder.ProductPrice,
                    ProductQuantity = menuOptionToOrder.ProductQuantity,
                    Subtotal = menuOptionToOrder.Subtotal
                });
            }

            return clientOrder;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrderByClientId(Guid clientId)
        {
            var orders = await  _orderRepository.GetOrderListByClientId(clientId);

            orders = orders.Where(o => o.OrderStatus == OrderStatus.Closed || o.OrderStatus == OrderStatus.Canceled)
                    .OrderByDescending(o => o.Code);

            if (!orders.Any()) return null;

            var orderViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                orderViewModel.Add(new OrderViewModel
                {
                    OrderId = order.Id,
                    Amount = order.Amount,
                    CreatedAt = order.CreatedAt,
                    ClientId = order.ClientId,
                    RestaurantId = order.RestaurantId,
                    TableId = order.TableId,
                    OrderStatus = order.OrderStatus
                });
            }

            return orderViewModel;
        }
    }
}
