using FTGO.OrderService.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FTGO.OrderService.Application;

public static class ApplicationDependencies 
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();
        
        return services;
    }
}