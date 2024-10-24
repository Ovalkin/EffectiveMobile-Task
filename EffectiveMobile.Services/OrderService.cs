using System.Text;
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

    public async Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrders(string cityDistrict,
        DateTime firstDeliveryDateTime)
    {
        var orders = RetrievedOrderDto.CreateDtos(await repo.RetrieveAllAsync());
        return orders
            .Where(o => o.CityDistrict == cityDistrict)
            .Where(o => o.DeliveryTime >= firstDeliveryDateTime
                        && o.DeliveryTime <= firstDeliveryDateTime.AddMinutes(30))
            .OrderBy(o => o.DeliveryTime)
            .AsEnumerable();
    }

    public async Task WriteToFileAsync(List<RetrievedOrderDto> orders, string path)
    {
        StringBuilder fileNameBuilder = new StringBuilder();
        fileNameBuilder.Append("Orders ");
        fileNameBuilder.Append(orders.First().DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"));
        fileNameBuilder.Append('-');
        fileNameBuilder.Append(orders.Last().DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"));
        fileNameBuilder.Append(".txt");
        string fileName = fileNameBuilder.ToString();
        using (var writer = new StreamWriter(Path.Combine(path, fileName)))
        {
            await writer.WriteLineAsync($"Доставка по району: {orders.First().CityDistrict}\n");
            foreach (var order in orders)
            {
                await writer.WriteLineAsync(
                    $"Номер заказа: {order.Id}, Вес: {order.Weight}КГ, Время доставки: {order.DeliveryTime}");
            }
        }
    }
}