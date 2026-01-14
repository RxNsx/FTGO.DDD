using FTGO.OrderService.Domain.Models;
using FTGO.OrderService.Domain.ValueObjects;
using SharedKernel.Abstractions;

namespace FTGO.OrderService.Domain.Aggregates;

public class Order : IAggregateRoot
{
    private Guid _userId;
    private decimal _orderTotalPrice;
    private uint _orderTotalQuantity;
    private DeliveryInfo? _deliveryInfo;
    private PaymentInfo? _paymentInfo;
    private OrderStatus _orderStatus;
    private List<OrderLineItem> _orderLineItems;

    public Guid Id { get; set; }
    public DeliveryInfo? DeliveryInfo => _deliveryInfo;
    public PaymentInfo? PaymentInfo => _paymentInfo;

    public decimal OrderTotalPrice
    {
        get => _orderTotalPrice;
        private init => _orderTotalPrice = value;
    }

    public uint OrderTotalQuantity
    {
        get => _orderTotalQuantity;
        private init => _orderTotalQuantity = value;
    }

    public Guid UserId
    {
        get => _userId;
        private init => _userId = value;
    }
    
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
    }

    //TODO: Следующая итерация
    private Order(DeliveryInfo? deliveryInfo, PaymentInfo paymentInfo, List<OrderLineItem> orderLineItems, Guid userId) : this(deliveryInfo, paymentInfo, orderLineItems)
    {
        _userId = userId;
    }
    
    public DeliveryInfo? GetDeliveryInfo() => _deliveryInfo;
    public void SetDeliveryInfo(DeliveryInfo newDeliveryInfo)
    {
        _deliveryInfo = newDeliveryInfo;
    }
    
    public PaymentInfo? GetPaymentInfo() => PaymentInfo;
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
            CalcOrderTotalValues();
        }
    }

    public void RemoveOrderLineItem(Guid productId)
    {
        var removeOrderLineItem = _orderLineItems.FirstOrDefault(x => x.ProductId == productId);
        if (removeOrderLineItem is not null)
        {
            _orderLineItems.Remove(removeOrderLineItem);
            CalcOrderTotalValues();
        }
    }

    public void ClearOrderLineItems()
    {
        _orderLineItems.Clear();
        CalcOrderTotalValues();
    }

    public static Order Create(DeliveryInfo? deliveryInfo, PaymentInfo? paymentInfo, List<OrderLineItem> orderLineItems)
    {
        return new Order(deliveryInfo, paymentInfo, orderLineItems);
    }

    private void CalcOrderTotalValues()
    {
        CalcOrderTotalPrice();
        CalcOrderTotalQuantity();
    }
    
    private void CalcOrderTotalPrice()
    {
        _orderTotalPrice = _orderLineItems.Sum(x => x.ProductPrice * x.Quantity);
    }

    private void CalcOrderTotalQuantity()
    {
        _orderTotalQuantity = (uint)_orderLineItems.Sum(x => x.Quantity);
    }
}