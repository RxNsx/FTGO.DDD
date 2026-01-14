namespace FTGO.OrderService.Application.Dtos.Order;

public class CreateOrderLineItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}