using FluentResults;

namespace FTGO.OrderService.Domain.ValueObjects;

public record PaymentInfo
{
    public string CardHolder { get; init; }
    public string CardNumber { get; init; }
    public DateOnly ExpiryDate { get; init; }

    private PaymentInfo() { }
    
    private PaymentInfo(string cardHolder, string cardNumber, DateOnly expiryDate)
    {
        CardHolder = cardHolder;
        CardNumber = cardNumber;
        ExpiryDate = expiryDate;
    }
    
    public static Result<PaymentInfo> Create(string cardHolder, string cardNumber, DateOnly expiryDate)
    {
        if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrWhiteSpace(cardNumber))
        {
            return Result.Fail($"{nameof(cardNumber)} cannot be null or empty");
        }
        if (string.IsNullOrEmpty(cardHolder) || string.IsNullOrWhiteSpace(cardHolder))
        {
            return Result.Fail($"{nameof(cardHolder)} cannot be null or empty");
        }
        if (expiryDate < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return Result.Fail($"{nameof(expiryDate)} cannot be smaller or current date");
        }
        
        return Result.Ok(new PaymentInfo(cardHolder, cardNumber, expiryDate));
    }
}