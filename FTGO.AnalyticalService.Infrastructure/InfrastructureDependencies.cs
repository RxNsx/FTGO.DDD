using ClickHouse.EntityFrameworkCore.Extensions;
using FTGO.AnalyticalService.Infrastructure.AppDbContext;
using Microsoft.Extensions.DependencyInjection;

namespace FTGO.AnalyticalService.Infrastructure;

public static class InfrastructureDependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AnalyticDbContext>(options =>
        {
            options.UseClickHouse("Host=localhost;Protocol=http;Port=8123;Database=QuickStart");
        });
        
        return services;
    }
}