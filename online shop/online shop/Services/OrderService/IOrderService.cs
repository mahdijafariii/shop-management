
using online_shop.Model;

namespace online_shop.Services.OrderService;

public interface IOrderService
{
    Task<Order> GetOrder(string authority);
    Task<(List<Order>,int)> GetUserOrders(string userId,int page , int limit);
    Task<(List<Order>, int)> GetAllOrders(int page, int limit);
    Task<Order> IsExistOrder(string authority);
    Task<Order> AddOrder(Order order);
}