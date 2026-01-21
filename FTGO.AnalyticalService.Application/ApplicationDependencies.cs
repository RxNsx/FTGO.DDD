using Microsoft.Extensions.DependencyInjection;

namespace FTGO.AnalyticalService.Application;

public static class ApplicationDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}