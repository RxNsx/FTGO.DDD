using FTGO.OrderService.Domain.ValueObjects;

namespace FTGO.OrderServiceTests.Unit.Order;

public class DeliveryInfoInvariantTests
{
    private const string CityName = "Main City";
    private const string EmptyCityName = "";
    
    private const string StreetName = "Main Street";
    private const string EmptyStreetName = "";

    private readonly DateTime _positiveDateDelivery = DateTime.UtcNow.AddDays(5);
    private readonly DateTime _shortDateDelivery = DateTime.UtcNow.AddDays(1);
    private readonly DateTime _currentDateDelivery = DateTime.UtcNow;
    private readonly DateTime _negativeDateDelivery = DateTime.UtcNow.AddDays(-1);
    
    [Fact]
    public void DeliveryInfo_ShouldNotHave_EmptyCity()
    {
        var deliveryInfo = DeliveryInfo.Create(EmptyCityName, StreetName, _positiveDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldNotHave_NullCity()
    {
        var deliveryInfo = DeliveryInfo.Create(null, StreetName, _positiveDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldNotHave_EmptyStreet()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, EmptyStreetName, _positiveDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldNotHave_NullStreet()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, null, _positiveDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldNotHave_PastDeliveryDateTime()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, StreetName, _negativeDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldNotHave_CurrentDeliveryDateTime()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, StreetName, _currentDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }
    
    [Fact]
    public void DeliveryInfo_ShouldNotHave_ShortDeliveryDateTime()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, StreetName, _shortDateDelivery);
        Assert.True(deliveryInfo.IsFailed);
    }

    [Fact]
    public void DeliveryInfo_ShouldCreate_Successfully()
    {
        var deliveryInfo = DeliveryInfo.Create(CityName, StreetName, _positiveDateDelivery);
        Assert.True(deliveryInfo.IsSuccess);
    }
}