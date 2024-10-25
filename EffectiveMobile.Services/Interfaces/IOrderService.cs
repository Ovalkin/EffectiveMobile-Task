using EffectiveMobile.Common.DTOs;

namespace EffectiveMobile.Services.Interfaces;

public interface IOrdersService
{
    Task<CreateOrderDto?> CreateOrderAsync(CreateOrderDto dto);
    Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrdersAsync(string cityDistrict, DateTime firstDeliveryDateTime);
    Task WriteToFileAsync(List<RetrievedOrderDto> orders, string path);
}