using FluentResults;

namespace FTGO.OrderService.Domain.ValueObjects;

public record DeliveryInfo
{
    public string Street { get; init; }
    public string City { get; init; }
    public DateTime DeliveryDateTime { get; init; }

    private DeliveryInfo() { }
    
    private DeliveryInfo(string city, string street, DateTime deliveryDateTime)
    {
        Street = street;
        City = city;
        DeliveryDateTime = deliveryDateTime;
    }
    
    public static Result<DeliveryInfo> Create(string city, string street, DateTime deliveryDateTime)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(city))
        {
            return Result.Fail($"{nameof(city)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
        {
            return Result.Fail($"{nameof(street)} cannot be null or empty");
        }
        if (deliveryDateTime < DateTime.UtcNow)
        {
            return Result.Fail($"{nameof(deliveryDateTime)} cannot be smaller or current date");
        }
        if (deliveryDateTime - DateTime.UtcNow < TimeSpan.FromDays(3))
        {
            return Result.Fail($"{nameof(deliveryDateTime)} cannot be smaler than 3 days to deliver");
        }
        
        return Result.Ok(new DeliveryInfo(city, street, deliveryDateTime));
    }
}