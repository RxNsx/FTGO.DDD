namespace FTGO.OrderService.Application.Dtos.Order;

public class OrderResponseDto
{
    public Guid OrderId { get; set; }
    public string ErrorMessage { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    
    public string CardHolder { get; set; }
    public string CardNumber { get; set; }
    public DateOnly ExpiryDate { get; set; }

    public List<OrderLineItemResponseDto> OrderLineItems { get; set; }
    public int TotalProductAmount { get; set; }
}