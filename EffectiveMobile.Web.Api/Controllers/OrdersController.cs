using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Common.EntityModel.Sqlite;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;
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
    public async Task<IActionResult> GetFiltered(string cityDistrict, DateTime firstDeliveryDateTime)
    {
        return Ok(await service.GetFilteredOrders(cityDistrict, firstDeliveryDateTime));
    }
}