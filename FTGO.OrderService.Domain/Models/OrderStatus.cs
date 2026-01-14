namespace FTGO.OrderService.Domain.Models;

public enum OrderStatus
{
    Created,
    Cancelled,
    Rejected,
    Pending,
    Verified,
    Shipped,
    Completed
}