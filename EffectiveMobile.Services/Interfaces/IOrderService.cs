using EffectiveMobile.Common.DTOs;

namespace EffectiveMobile.Services.Interfaces;

public interface IOrdersService
{
    Task<CreateOrderDto?> CreateOrder(CreateOrderDto dto);
    Task<IEnumerable<RetrievedOrderDto?>> GetFilteredOrders(string cityDistrict, DateTime firstDeliveryDateTime);
}