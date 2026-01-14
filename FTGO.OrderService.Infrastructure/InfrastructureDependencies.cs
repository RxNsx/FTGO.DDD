using FTGO.OrderService.Infrastructure.AppContext;
using FTGO.OrderService.Infrastructure.Interfaces;
using FTGO.OrderService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FTGO.OrderService.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql("Server=localhost;Database=ftgo.orderservice;User ID=postgres;Password=-pl,mju7");
            options.EnableSensitiveDataLogging(false);
        });
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}