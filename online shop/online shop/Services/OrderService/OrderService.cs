using online_shop.DTO;
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

    public async Task<(List<Order>,int)> GetUserOrders(string userId, int page, int limit)
    {
        var orders =await _orderRepository.GetAllUserOrdersAsync(userId, page, limit);
        var count = await _orderRepository.OrdersOfUserTotalCount(userId);
        return (orders,count);
    }

    public async Task<(List<Order>,int)> GetAllOrders(int page, int limit)
    {
        var orders = await _orderRepository.GetAllOrdersAsync(page, limit);
        if (!orders.Any())
        {
            throw new NotFoundException("Order");
        }
        
        var totalCount = await _orderRepository.OrdersTotalCount();
        return (orders, totalCount);
    }

    public async Task<Order> IsExistOrder(string authority)
    {
        var order = await _orderRepository.GetOrderAsync(authority);
        if (order is null)
        {
            return null;
        }
        return order;
    }

    public async Task<Order> AddOrder(Order order)
    {
        var checkoutRes = await _orderRepository.AddOrderAsync(order);
        return checkoutRes;
    }

    public async Task UpdateOrder(UpdateOrderDto request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order is null)
        {
            throw new NotFoundException("Order");
        }

        var check = await _orderRepository.UpdateOrderAsync(request);
        if (!check)
        {
            throw new InvalidRequestException("Update was not successful!", 400);
        }
    }
}