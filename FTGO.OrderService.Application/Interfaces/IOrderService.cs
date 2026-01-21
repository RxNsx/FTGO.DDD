using FluentResults;
using FTGO.OrderService.Application.Dtos.Order;
using FTGO.OrderService.Domain.Aggregates;

namespace FTGO.OrderService.Application.Interfaces;

public interface IOrderService
{
    Task<Result<List<OrderResponseDto>>> GetAllAsync();
    Task<Result<OrderResponseDto>> GetAsync(OrderFinderDto orderFinderDto);
    Task<Result<OrderResponseDto>> CreateAsync(OrderCreateDto orderCreateDto);
    Task<Result<bool>> DeleteAsync(OrderDeleteDto orderDeleteDto);
}