using FTGO.OrderService.Domain.ValueObjects;

namespace FTGO.OrderServiceTests.Unit.Order;

public class PaymentInfoInvariantTests
{
    private const string CardHolder = "Correct CartHolder";
    private const string WhiteSpaceCardHolder = " ";
    private const string? NullCardHolder = null;
    private const string EmptyCardHolder = "";
    
    private const string CardNumber = "123123123";
    private const string WhiteSpaceCardNumber = " ";
    private const string? NullCardNumber = null;
    private const string EmptyCardNumber = "";
    
    private readonly DateOnly _positiveExpiryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1));
    private readonly DateOnly _negativeExpiryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));
    private readonly DateOnly? _nullExpiryDate = null;
    
    [Fact]
    public void PaymentInfo_ShouldNotHave_EmptyCardHolder()
    {
        var paymentInfo = PaymentInfo.Create(EmptyCardHolder, CardNumber, _positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_NullCardHolder()
    {
        var paymentInfo = PaymentInfo.Create(NullCardHolder!, CardNumber ,_positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_WhitespaceCardHolder()
    {
        var paymentInfo = PaymentInfo.Create(WhiteSpaceCardHolder, CardNumber, _positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_EmptyCardNumber()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, EmptyCardNumber, _positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_NullCardNumber()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, NullCardNumber!, _positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_WhitespaceCardNumber()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, WhiteSpaceCardNumber, _positiveExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldNotHave_PastExpiryDate()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, CardNumber, _negativeExpiryDate);
        Assert.True(paymentInfo.IsFailed);
    }

    [Fact]
    public void PaymentInfo_ShouldAllow_CurrentExpiryDate()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, CardNumber, DateOnly.FromDateTime(DateTime.UtcNow));
        Assert.True(paymentInfo.IsSuccess);
    }

    [Fact]
    public void PaymentInfo_ShouldCreate_Successfully()
    {
        var paymentInfo = PaymentInfo.Create(CardHolder, CardNumber, _positiveExpiryDate);
        Assert.True(paymentInfo.IsSuccess);
    }
}