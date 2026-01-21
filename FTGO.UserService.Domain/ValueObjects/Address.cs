using FluentResults;

namespace FTGO.UserService.Domain.ValueObjects;

public record Address
{
    public string City { get; init; }
    public string Street { get; init; }
    public string HouseNumber { get; init; }
    public string ApartmentNumber { get; init; }
    public int Entrance { get; init; }
    public int Floor { get; init; }
    public string Details { get; init; }
    
    private Address() { }

    private Address(string city, string street, string houseNumber, int entrance, int floor, string details)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        Entrance = entrance;
        Floor = floor;
        Details = details;
    }

    public static Result<Address> Create(string city, string street, string houseNumber, int entrance, int floor, string details)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(city))
        {
            return Result.Fail($"{nameof(city)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(street) || string.IsNullOrWhiteSpace(street))
        {
            return Result.Fail($"{nameof(street)} cannot be null or empty");
        }
        if (entrance <= 0)
        {
            return Result.Fail($"{nameof(entrance)} must be greater than 0");
        }
        
        return Result.Ok(new Address(city, street, houseNumber, entrance, floor, details));
    }
}