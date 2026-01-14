namespace FTGO.OrderService.Application.Dtos.Order;

public class OrderCreateDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    
    public string CardHolder { get; set; }
    public string CardNumber { get; set; }
    public DateOnly ExpiryDate { get; set; }
    
    public List<CreateOrderLineItemDto> OrderLineItems { get; set; }
}