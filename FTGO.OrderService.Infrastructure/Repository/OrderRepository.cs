using FTGO.OrderService.Domain.Aggregates;
using FTGO.OrderService.Infrastructure.AppContext;
using FTGO.OrderService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FTGO.OrderService.Infrastructure.Repository;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    public async Task<List<OrderAggregate>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task<OrderAggregate?> GetAsync(Guid orderId)
    {
        return await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<OrderAggregate> CreateAsync(OrderAggregate order)
    {
        var newOrder = await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
        
        return newOrder.Entity;
    }

    public async Task<bool> DeleteAsync(Guid orderId)
    {
        var orderToDelete = await GetAsync(orderId);
        if (orderToDelete is null)
        {
            return false;
        }
        
        try
        {
            context.Orders.Remove(orderToDelete);
            await context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}