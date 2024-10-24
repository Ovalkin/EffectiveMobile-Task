using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;
using EffectiveMobile.Repositories.Interfaces;
using EffectiveMobile.Services.Interfaces;
using DateTime = System.DateTime;

namespace EffectiveMobile.Services;

public class OrdersService(IOrdersRepository repo) : IOrdersService
{
    public async Task<CreateOrderDto?> CreateOrder(CreateOrderDto dto)
    {
        Order? order = await repo.CreateAsync(CreateOrderDto.CreateEntity(dto));
        return order != null ? CreateOrderDto.CreateDto(order) : null;
    }

    public async Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrders()
    {
        var orders = RetrievedOrderDto.CreateDtos(await repo.RetrieveAllAsync());
        var filteredOrders = orders
            .GroupBy(o => o.CityDistrict)
            .OrderByDescending(o => o.Count())
            .First()
            .Select(o => o)
            .AsEnumerable()
            .OrderBy(o => o.DeliveryTime);
        return filteredOrders;
    }

    public async Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrders(string cityDistrict, DateTime dateTime)
    {
        var orders = RetrievedOrderDto.CreateDtos(await repo.RetrieveAllAsync());
        var filteredOrders = orders
            .Where(o => o.CityDistrict == cityDistrict)
            .Where(o => o.DeliveryTime >= dateTime)
            .OrderBy(o => o.DeliveryTime)
            .ToList();
        DateTime firstOrderDeliveryTime = filteredOrders.First().DeliveryTime;
        return filteredOrders
            .Where(o => o.DeliveryTime <= firstOrderDeliveryTime.AddMinutes(30))
            .AsEnumerable();
    }
}