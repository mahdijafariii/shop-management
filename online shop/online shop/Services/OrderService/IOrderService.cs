
using online_shop.Model;

namespace online_shop.Services.OrderService;

public interface IOrderService
{
    Task<Order> GetOrder(string authority);
    Task<Order> IsExistOrder(string authority);
    Task<Order> AddOrder(Order order);
}