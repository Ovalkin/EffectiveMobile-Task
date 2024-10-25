using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrdersService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        if (!ModelState.IsValid) return BadRequest();
        var created = await service.CreateOrder(orderDto);
        if (created == null) return BadRequest();
        return Ok(created);
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(string cityDistrict, DateTime firstDeliveryDateTime, string path)
    {
        var orders = (await service.GetFilteredOrders(cityDistrict, firstDeliveryDateTime))
            .ToList();
        if (orders.First() == null)
            return BadRequest("В ближайшие 30 минут от указанного времени нет заказов в указанном районе!");
        _ = service.WriteToFileAsync(orders!, path);
        return Ok(orders);
    }
}