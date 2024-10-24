using EffectiveMobile.Common.EntityModel.Sqlite.Entities;

namespace EffectiveMobile.Common.DTOs;

public class CreateOrderDto
{
    public double Weight { get; set; }

    public string CityDistrict { get; set; } = null!;
    
    public DateTime DeliveryTime { get; set; }

    public static Order CreateEntity(CreateOrderDto dto)
    {
        return new Order
        {
            Weight = dto.Weight,
            CityDistrict = dto.CityDistrict,
            DeliveryTime = dto.DeliveryTime
        };
    }
    
    public static CreateOrderDto CreateDto(Order dto)
    {
        return new CreateOrderDto
        {
            Weight = dto.Weight,
            CityDistrict = dto.CityDistrict,
            DeliveryTime = dto.DeliveryTime
        };
    }
}