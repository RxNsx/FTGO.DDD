using FTGO.OrderService.Application.Dtos.Order;
using FTGO.OrderService.Application.Interfaces;
using FTGO.OrderService.Domain.Aggregates;
using FTGO.OrderService.Domain.ValueObjects;
using FTGO.OrderService.Infrastructure.Interfaces;

namespace FTGO.OrderService.Application.Services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        var allOrders = await orderRepository.GetAllAsync();
        var allOrdersDto = allOrders
            .Select(x => new OrderResponseDto
            {
                OrderId = x.Id,
                Street = x.DeliveryInfo!.Street,
                City = x.DeliveryInfo.City,
                DeliveryDateTime = x.DeliveryInfo.DeliveryDateTime,
                CardHolder = x.PaymentInfo!.CardHolder,
                CardNumber = x.PaymentInfo!.CardNumber,
                ExpiryDate = x.PaymentInfo!.ExpiryDate,
                OrderLineItems = x.OrderLineItems
                    .Select(t => new OrderLineItemResponseDto
                    {
                        ProductId = t.ProductId,
                        ProductName = t.ProductName,
                        ProductPrice = t.ProductPrice,
                        Quantity = t.Quantity
                    })
                    .ToList(),
                TotalProductAmount = x.OrderLineItems.Sum(t => t.Quantity)
            })
            .ToList();
        
        return allOrdersDto;
    }

    public async Task<OrderResponseDto> GetAsync(OrderFinderDto orderFinderDto)
    {
        var orderToFind = await orderRepository.GetAsync(orderFinderDto.OrderId);
        if (orderToFind is null)
        {
            return new OrderResponseDto()
            {
                ErrorMessage = "Order not found"
            };
        }
        
        return new OrderResponseDto
        {
            OrderId = orderToFind.Id,
            Street = orderToFind.DeliveryInfo!.Street,
            City = orderToFind.DeliveryInfo.City,
            DeliveryDateTime = orderToFind.DeliveryInfo.DeliveryDateTime,
            CardHolder = orderToFind.PaymentInfo!.CardHolder,
            CardNumber = orderToFind.PaymentInfo!.CardNumber,
            ExpiryDate = orderToFind.PaymentInfo!.ExpiryDate,
            OrderLineItems = orderToFind.OrderLineItems
                .Select(t => new OrderLineItemResponseDto
                {
                    ProductId = t.ProductId,
                    ProductName = t.ProductName,
                    ProductPrice = t.ProductPrice,
                    Quantity = t.Quantity
                })
                .ToList(),
            TotalProductAmount = orderToFind.OrderLineItems.Sum(t => t.Quantity)
        };
    }

    public async Task<OrderResponseDto> CreateAsync(OrderCreateDto orderCreateDto)
    {
        var paymentInfo = PaymentInfo.Create(orderCreateDto.CardHolder, orderCreateDto.CardNumber, orderCreateDto.ExpiryDate);
        var deliveryInfo = DeliveryInfo.Create(orderCreateDto.City, orderCreateDto.Street, orderCreateDto.DeliveryDateTime);
        var orderLineItems = orderCreateDto.OrderLineItems
            .Select(x => OrderLineItem.Create(x.ProductId, x.ProductName, x.ProductPrice, x.Quantity).Value)
            .ToList();
        var newOrder = Order.Create(deliveryInfo.Value, paymentInfo.Value, orderLineItems);
        
        var createdOrder = await orderRepository.CreateAsync(newOrder);
        return new OrderResponseDto
        {
            OrderId = createdOrder.Id,
            Street = createdOrder.DeliveryInfo!.Street,
            City = createdOrder.DeliveryInfo.City,
            DeliveryDateTime = createdOrder.DeliveryInfo.DeliveryDateTime,
            CardHolder = createdOrder.PaymentInfo!.CardHolder,
            CardNumber = createdOrder.PaymentInfo!.CardNumber,
            ExpiryDate = createdOrder.PaymentInfo!.ExpiryDate,
            OrderLineItems = createdOrder.OrderLineItems
                .Select(t => new OrderLineItemResponseDto
                {
                    ProductId = t.ProductId,
                    ProductName = t.ProductName,
                    ProductPrice = t.ProductPrice,
                    Quantity = t.Quantity
                })
                .ToList(),
            TotalProductAmount = createdOrder.OrderLineItems.Sum(t => t.Quantity)
        };
    }

    public async Task DeleteAsync(OrderDeleteDto orderDeleteDto)
    {
        await orderRepository.DeleteAsync(orderDeleteDto.OrderId); 
    }
}