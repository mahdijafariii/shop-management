using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories;

namespace online_shop.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> GetOrder(string authority)
    {
        var order = await _orderRepository.GetOrderAsync(authority);
        if (order is null)
        {
            throw new NotFoundException("Order");
        }
        return order;
    }
}