using FTGO.OrderService.Domain.Aggregates;

namespace FTGO.OrderService.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetAsync(Guid orderId);
    Task<Order> CreateAsync(Order order);
    Task DeleteAsync(Guid orderId);
}