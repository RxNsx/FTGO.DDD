using FTGO.OrderService.Application.Dtos.Order;
using FTGO.OrderService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FTGO.OrderService.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [Route("[action]")]
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] OrderFinderDto orderFinderDto)
    {
        var result = await orderService.GetAsync(orderFinderDto);
        if (!string.IsNullOrEmpty(result.ErrorMessage))
        {
            return NotFound(new OrderErrorResponse()
            {
                ErrorMessage = result.ErrorMessage,
            });
        }
        return Ok(result);
    }

    [Route("[action]")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrderCreateDto orderCreateDto)
    {
        var newOrder = await orderService.CreateAsync(orderCreateDto);      
        return Ok(newOrder);
    }

    [Route("[action]")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(OrderDeleteDto orderDeleteDto)
    {
        await orderService.DeleteAsync(orderDeleteDto);
        return Ok();
    }
}