using MealCenter.Orders.Application.Queries.ViewModels;

namespace MealCenter.Orders.Application.Queries
{
    public interface IOrderQueries
    {
        Task<IEnumerable<OrderViewModel>> GetOrderByClientId(Guid clientId);
        Task<ClientOrderViewModel> GetClientOrder(Guid clientId);
    }
}
