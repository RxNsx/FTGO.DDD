using FTGO.OrderService.Application.Dtos.Order;
using FTGO.OrderService.Domain.Aggregates;

namespace FTGO.OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponseDto>> GetAllAsync();
    Task<OrderResponseDto> GetAsync(OrderFinderDto orderFinderDto);
    Task<OrderResponseDto> CreateAsync(OrderCreateDto orderCreateDto);
    Task DeleteAsync(OrderDeleteDto orderDeleteDto);
}