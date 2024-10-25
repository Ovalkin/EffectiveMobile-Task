using EffectiveMobile.Common.EntityModel.Sqlite.Entities;

namespace EffectiveMobile.Common.DTOs;

public class RetrievedOrderDto
{
    public int Id { get; set; }

    public double Weight { get; set; }

    public string CityDistrict { get; set; } = null!;

    public DateTime DeliveryTime { get; set; }

    public static IEnumerable<RetrievedOrderDto> CreateDtos(IEnumerable<Order?> orders)
    {
        List<RetrievedOrderDto> dtos = [];
        foreach (var order in orders)
        {
            if (order is null) return [];
            dtos.Add(new RetrievedOrderDto
            {
                Id = order.Id,
                Weight = order.Weight,
                CityDistrict = order.CityDistrict,
                DeliveryTime = order.DeliveryTime
            });
        }

        return dtos.AsEnumerable();
    }
}