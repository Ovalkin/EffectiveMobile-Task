using System.Text;
using EffectiveMobile.Common.DTOs;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;
using EffectiveMobile.Repositories.Interfaces;
using EffectiveMobile.Services.Interfaces;
using Microsoft.Extensions.Logging;
using DateTime = System.DateTime;

namespace EffectiveMobile.Services;

public class OrdersService(IOrdersRepository repo, ILogger<OrdersService> logger) : IOrdersService
{
    public async Task<CreateOrderDto?> CreateOrderAsync(CreateOrderDto dto)
    {
        Order? order = await repo.CreateAsync(CreateOrderDto.CreateEntity(dto));
        return order != null ? CreateOrderDto.CreateDto(order) : null;
    }

    public async Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrdersAsync(string cityDistrict,
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
        string fileName = GenerateFileName(orders.First(), orders.Last());
        fileName = Path.Combine(path, fileName);
        logger.LogInformation($"Создание файла списка заказов по пути {fileName}");
        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                await writer.WriteLineAsync($"Доставка по району: {orders.First().CityDistrict}\n");
                foreach (var order in orders)
                {
                    await writer.WriteLineAsync(
                        $"Номер заказа: {order.Id}, Вес: {order.Weight}КГ, Время доставки: {order.DeliveryTime}");
                }
            }
        }
        catch (IOException e)
        {
            logger.LogError("Ошибка ввода-вывода при записи в файл: " + e.Message);
        }
        catch (TaskCanceledException e)
        {
            logger.LogError("Запись в файл была прервана: " + e.Message);
        }
    }

    private static string GenerateFileName(RetrievedOrderDto firstOrder, RetrievedOrderDto lastOrder)
    {
        StringBuilder fileNameBuilder = new StringBuilder();
        fileNameBuilder.Append("Orders ");
        fileNameBuilder.Append(firstOrder.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"));
        fileNameBuilder.Append('-');
        fileNameBuilder.Append(lastOrder.DeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"));
        fileNameBuilder.Append(".txt");
        return fileNameBuilder.ToString();
    }
}