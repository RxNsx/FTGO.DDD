using DotNet.Testcontainers.Builders;
using FTGO.OrderService.Infrastructure.AppContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;

namespace FTGO.OrderServiceTests.Factories;

public class WebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithCleanUp(true)
        .WithImage("postgres:17")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithDatabase("test_database")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilDatabaseIsAvailable(NpgsqlFactory.Instance).UntilExternalTcpPortIsAvailable(5432))
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<OrderDbContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddLogging();
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
    }
}
