using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Repositories;

public interface IOrderRepository
{
    Task<Order> GetOrderAsync(string authority);
    Task<Order> GetOrderByIdAsync(string id);
    Task<Order> AddOrderAsync(Order order);
    Task<List<Order>> GetAllOrdersAsync(int page, int limit);
    Task<List<Order>> GetAllUserOrdersAsync(string userId, int page, int limit);
    Task<int> OrdersOfUserTotalCount(string userId);
    Task<int> OrdersTotalCount();
    Task<bool> UpdateOrderAsync(UpdateOrderDto orderDto);


}