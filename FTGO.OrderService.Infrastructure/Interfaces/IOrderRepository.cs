using FTGO.OrderService.Domain.Aggregates;

namespace FTGO.OrderService.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderAggregate>> GetAllAsync();
    Task<OrderAggregate?> GetAsync(Guid orderId);
    Task<OrderAggregate> CreateAsync(OrderAggregate order);
    Task<bool> DeleteAsync(Guid orderId);
}