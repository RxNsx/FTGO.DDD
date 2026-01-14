using FluentResults;

namespace FTGO.OrderService.Domain.ValueObjects;

public record OrderLineItem
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }

    private OrderLineItem() { }
    
    private OrderLineItem(Guid productId, string productName, decimal productPrice, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        ProductPrice = productPrice;
        Quantity = quantity;
    }
    
    public static Result<OrderLineItem> Create(Guid productId, string productName, decimal productPrice, int quantity)
    {
        if (productId == Guid.Empty)
        {
            return Result.Fail($"{nameof(productId)} cannot be empty");
        }
        if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
        {
            return Result.Fail($"{nameof(productName)} cannot be empty");
        }
        if (productPrice <= 0)
        {
            return Result.Fail($"{nameof(productPrice)} cannot be zero or negative");
        }
        if (quantity <= 0)
        {
            return Result.Fail($"{nameof(quantity)} cannot be zero or negative");
        }

        return Result.Ok(new OrderLineItem(productId, productName, productPrice, quantity));
    }
    
    public decimal GetTotalAmount() => ProductPrice * Quantity;
}