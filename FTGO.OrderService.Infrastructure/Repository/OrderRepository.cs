using FTGO.OrderService.Domain.Aggregates;
using FTGO.OrderService.Infrastructure.AppContext;
using FTGO.OrderService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FTGO.OrderService.Infrastructure.Repository;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<Order?> GetAsync(Guid orderId)
    {
        return await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        var newOrder = await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        
        return newOrder.Entity;
    }

    public async Task DeleteAsync(Guid orderId)
    {
        var orderToDelete = await GetAsync(orderId);
        if (orderToDelete is not null)
        {
            context.Orders.Remove(orderToDelete);
            await context.SaveChangesAsync();
        }
    }
}