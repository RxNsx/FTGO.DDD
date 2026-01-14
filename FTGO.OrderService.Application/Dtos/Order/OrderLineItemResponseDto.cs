namespace FTGO.OrderService.Application.Dtos.Order;

public class OrderLineItemResponseDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
}