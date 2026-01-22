using FTGO.OrderService.Application.Dtos.Order;
using FTGO.OrderService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FTGO.OrderService.API.Controllers;

[Route("api/v1/orders/order")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [Route("{orderId:guid}")]
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromRoute] Guid orderId)
    {
        var result = await orderService.GetAsync(new OrderFinderDto() { OrderId = orderId });
        if (!result.IsSuccess)
        {
            return NotFound();
        }
        
        return Ok(result.Value);
    }

    [Route("create")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]OrderCreateDto orderCreateDto)
    {
        var result = await orderService.CreateAsync(orderCreateDto);
        if (!result.IsSuccess)
        {
            return BadRequest(string.Join(',', result.Errors.Select(x => x.Message).ToList()));
        }
        
        return Created(new Uri("api/v1/orders/order/" + result.Value.OrderId), result.Value);
    }

    [Route("{orderId:guid}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid orderId)
    {
        var result = await orderService.DeleteAsync(new OrderDeleteDto() { OrderId = Guid.Empty });
        if (!result.IsSuccess)
        {
            return BadRequest(string.Join(',', result.Errors.Select(x => x.Message).ToList()));
        }

        return Ok();
    }
}