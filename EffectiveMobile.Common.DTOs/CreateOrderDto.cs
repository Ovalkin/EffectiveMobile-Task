using System.ComponentModel.DataAnnotations;
using EffectiveMobile.Common.EntityModel.Sqlite.Entities;

namespace EffectiveMobile.Common.DTOs;

public class CreateOrderDto
{
    [Range(0.1, 200, ErrorMessage = "Вес должен быть от 0.1кг до 200кг!")]
    public double Weight { get; set; }

    [RegularExpression(@"^\D*$", ErrorMessage = "Имя района может содержать только слова")]
    public string CityDistrict { get; set; } = null!;
    
    [DataType(DataType.DateTime, ErrorMessage = "Дата должна быть в формате yyyy-MM-ddTHH:mm:ss")]
    [Range(typeof(DateTime), "2024-10-24 00:00:01", "2024-12-31 00:00:01", 
        ErrorMessage = "Дата доставки должна быть в диапазоне от 2024-10-24 00:00:01 до 2024-12-31 00:00:01")]
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