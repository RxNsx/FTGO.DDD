using FTGO.OrderServiceTests.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace FTGO.OrderServiceTests.Abstractions;

public class BaseIntegrationTest : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    
    public BaseIntegrationTest(WebAppFactory factory)
    {
        _factory = factory;
        var scope = _factory.Services.CreateScope();
    }
}