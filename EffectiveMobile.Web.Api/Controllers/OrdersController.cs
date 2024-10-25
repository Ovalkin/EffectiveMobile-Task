using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrdersService service, ILogger<OrdersController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
    {
        logger.LogInformation("Создание заказа");
        logger.LogInformation($"Входные данные: {orderDto.Weight} {orderDto.CityDistrict} {orderDto.DeliveryTime}");
        if (!ModelState.IsValid) return BadRequest();
        var created = await service.CreateOrderAsync(orderDto);
        if (created == null) return BadRequest();
        logger.LogInformation("Заказ добавлен");
        return Ok(created);
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(string cityDistrict, DateTime firstDeliveryDateTime, string path)
    {
        logger.LogInformation($"Создание списка заказов в район: {cityDistrict} начиная с {firstDeliveryDateTime}");
        var orders = (await service.GetFilteredOrdersAsync(cityDistrict, firstDeliveryDateTime))
            .ToList();
        if (orders.FirstOrDefault() is null)
            return BadRequest("В ближайшие 30 минут от указанного времени нет заказов в указанном районе!");
        _ = service.WriteToFileAsync(orders!, path);
        logger.LogInformation("Список создан");
        return Ok(orders);
    }
}