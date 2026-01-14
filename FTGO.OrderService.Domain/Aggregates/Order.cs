using FTGO.OrderService.Domain.Abstractions;
using FTGO.OrderService.Domain.Models;
using FTGO.OrderService.Domain.ValueObjects;

namespace FTGO.OrderService.Domain.Aggregates;

public class Order : Entity, IAggregateRoot
{
    private decimal _totalAmount;
    private DeliveryInfo? _deliveryInfo;
    private PaymentInfo? _paymentInfo;
    private OrderStatus _orderStatus;
    private List<OrderLineItem> _orderLineItems;

    public DeliveryInfo? DeliveryInfo => _deliveryInfo;
    public PaymentInfo? PaymentInfo => _paymentInfo;
    public decimal TotalAmount => _totalAmount;
    public IReadOnlyCollection<OrderLineItem> OrderLineItems => _orderLineItems;
    public OrderStatus OrderStatus => _orderStatus;
    
    private Order() { }

    private Order(DeliveryInfo? deliveryInfo, PaymentInfo paymentInfo, List<OrderLineItem> orderLineItems)
    {
        Id = Guid.NewGuid();
        _deliveryInfo = deliveryInfo;
        _paymentInfo = paymentInfo;
        _orderStatus = OrderStatus.Created;
        _orderLineItems = orderLineItems;
        _totalAmount = CalcTotalAmount();
    }
    
    public IEnumerable<OrderLineItem> GetOrderLineItems() => _orderLineItems;
    public decimal GetTotalAmount() => _totalAmount;
    
    public DeliveryInfo? GetDeliveryInfo() => _deliveryInfo;
    public void SetDeliveryInfo(DeliveryInfo newDeliveryInfo)
    {
        _deliveryInfo = newDeliveryInfo;
    }
    
    public PaymentInfo? GetPaymentInfo() => _paymentInfo;
    public void SetPaymentInfo(PaymentInfo newPaymentInfo)
    {
        _paymentInfo = newPaymentInfo;
    }

    public void AddOrderLineItem(Guid productId, string productName, decimal productPrice, int quantity)
    {
        var newOrderLineItem = OrderLineItem.Create(productId, productName, productPrice, quantity);
        if (newOrderLineItem.IsSuccess)
        {
            _orderLineItems.Add(newOrderLineItem.Value);
            _totalAmount = _orderLineItems.Sum(x => x.GetTotalAmount());
        }
    }

    public void RemoveOrderLineItem(Guid productId)
    {
        var removeOrderLineItem = _orderLineItems.FirstOrDefault(x => x.ProductId == productId);
        if (removeOrderLineItem is not null)
        {
            _orderLineItems.Remove(removeOrderLineItem);
            _totalAmount = CalcTotalAmount();
        }
    }

    public void ClearOrderLineItems()
    {
        _orderLineItems.Clear();
    }

    public static Order Create(DeliveryInfo? deliveryInfo, PaymentInfo? paymentInfo, List<OrderLineItem> orderLineItems)
    {
        return new Order(deliveryInfo, paymentInfo, orderLineItems);
    }
    
    private decimal CalcTotalAmount() => _orderLineItems.Sum(x => x.ProductPrice * x.Quantity);
}