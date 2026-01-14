using FTGO.OrderService.Domain.ValueObjects;

namespace FTGO.OrderServiceTests.Unit.Order;

public class OrderLineItemInvariantTests
{
    private readonly Guid _productId = Guid.NewGuid();
    private readonly Guid _emptyProductId = Guid.Empty;
    
    private const string ProductName = "ProductName";
    private const string EmptyProductName = "";
    private const string? NullProductName = null;
    
    private const decimal PositiveProductPrice = 10m;
    private const decimal ZeroProductPrice = 0m;
    private const decimal NegativeProductPrice = -10m;
    
    private const int PositiveQuantity = 1;
    private const int NegativeQuantity = -1;
    private const int ZeroQuantity = 0;
    
    [Fact]
    public void OrderLineItem_ShouldNotHave_EmptyProductId()
    {
        var orderLineItem = OrderLineItem.Create(_emptyProductId, ProductName, PositiveProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsFailed);
    }

    [Fact]
    public void OrderLineItem_ShouldNotHave_NegativeProductPrice()
    {
        var orderLineItem = OrderLineItem.Create(_productId, ProductName, NegativeProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsFailed);
    }
    
    [Fact]
    public void OrderLineItem_ShouldNotHave_ZeroProductPrice()
    {
        var orderLineItem = OrderLineItem.Create(_productId, ProductName, ZeroProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsFailed);
    }
    
    [Fact]
    public void OrderLineItem_ShouldNotHave_NegativeQuantity()
    {
        var orderLineItem = OrderLineItem.Create(_productId, ProductName, PositiveProductPrice, NegativeQuantity);
        Assert.True(orderLineItem.IsFailed);
    }

    [Fact]
    public void OrderLineItem_ShouldNotHave_ZeroQuantity()
    {
        var orderLineItem = OrderLineItem.Create(_productId, ProductName, PositiveProductPrice, ZeroQuantity);
        Assert.True(orderLineItem.IsFailed);
    }

    [Fact]
    public void OrderLineItem_ShouldNotHave_EmptyProductName()
    {
        var orderLineItem = OrderLineItem.Create(_productId, EmptyProductName, PositiveProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsFailed);
    }
    
    [Fact]
    public void OrderLineItem_ShouldNotHave_NullProductName()
    {
        var orderLineItem = OrderLineItem.Create(_productId, NullProductName!, PositiveProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsFailed);
    }
    
    [Fact]
    public void OrderLineItem_ShouldCreate_Successfully()
    {
        var orderLineItem = OrderLineItem.Create(_productId, ProductName, PositiveProductPrice, PositiveQuantity);
        Assert.True(orderLineItem.IsSuccess);
    }
}