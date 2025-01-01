using online_shop.Model;

namespace online_shop.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(string authority);
    Task<Order> AddOrderAsync(Order order);
}