using FTGO.OrderServiceTests.Abstractions;
using FTGO.OrderServiceTests.Factories;

namespace FTGO.OrderServiceTests.Integration;

public class TestCase(WebAppFactory webAppFactory) : BaseIntegrationTest(webAppFactory)
{
    [Fact]
    public async Task TestGet()
    {
        Assert.True(true);
    }
}